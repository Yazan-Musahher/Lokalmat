import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useCart } from '../../../contexts/CartContext';
import { useAuth } from '../../../contexts/AuthContext';
import { AiOutlineLoading3Quarters } from 'react-icons/ai';

const CartModal = ({ isOpen, closeCart }) => {
  const navigate = useNavigate();
  const { cartItems, removeFromCart } = useCart();
  const [isCheckoutLoading, setIsCheckoutLoading] = useState(false); // New state for tracking loading state

  const handleCheckout = async () => {
    setIsCheckoutLoading(true); // Begin loading effect
    if (!localStorage.getItem('token')) { // Using localStorage to check authentication
      alert('Please log in to continue.');
      closeCart();
      navigate('/login/');  // Redirect to login page if not authenticated
      return;
    }
  
    const productIds = cartItems.map(item => item.id); // This should be an array of GUIDs
    const userId = localStorage.getItem('userId'); // Fetching userId directly from localStorage
  
    try {
      const response = await fetch('http://localhost:5176/api/Payment/create-checkout-session', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ productIds, userId }), // Ensure keys match those expected by the backend
      });

      console.log("Product IDs:", JSON.stringify(productIds));  // Check the format of IDs in the console
      console.log("User ID:", userId);  // Verify that userId is logged correctly
  
      if (!response.ok) {
        const errorData = await response.json();
        console.error('Failed to create checkout session:', errorData);
        alert(`There was a problem with your payment: ${errorData.error}`);
        setIsCheckoutLoading(false); // Stop loading effect on error
        return;
      }
  
      const { url } = await response.json();
      if (url) {
        window.location.href = url; 
      } else {
        throw new Error('Payment URL is missing');
      }
      setIsCheckoutLoading(false);
    } catch (error) {
      console.error('Failed to create checkout session:', error);
      alert('There was a problem with your payment. Please try again.');
      setIsCheckoutLoading(false);
    }
  };

  if (!isOpen) {
    return null;
  }

  return (
    <div className="fixed inset-0 z-10 bg-black bg-opacity-50 backdrop-blur-sm flex justify-center items-center">
      <div className="bg-white p-6 rounded-lg shadow-lg space-y-4 w-full max-w-md mx-auto">
        <div className="flex flex-col divide-y divide-gray-200">
          {cartItems.map(item => (
            <div key={item.id} className="flex justify-between items-center py-2">
              <img src={item.imageUrl} alt={item.name} className="w-16 h-16 object-cover mr-2" />
              <span className="font-semibold">{item.name}</span>
              <span>{item.quantity}x</span>
              <span className="font-semibold">{item.price} kr</span>
              <button onClick={() => removeFromCart(item.id)} className="text-white bg-red-500 hover:bg-red-700 px-3 py-1 rounded">Fjern</button>
            </div>
          ))}
        </div>
        <div className="flex justify-end space-x-4 mt-4">
          <button onClick={closeCart} className="text-gray-700 bg-gray-200 hover:bg-gray-300 px-4 py-2 rounded shadow">Avbryt</button>
          <button 
          onClick={handleCheckout} 
          className="group relative flex justify-center items-center text-white bg-green-600 hover:bg-green-700 px-4 py-2 rounded shadow" 
          disabled={isCheckoutLoading}
        >
          {isCheckoutLoading ? (
            <AiOutlineLoading3Quarters className="animate-spin h-5 w-5 mr-2" />
          ) : (
            'Proceed to Checkout'
          )}
        </button>
        </div>
      </div>
    </div>
  );
};

export default CartModal;
