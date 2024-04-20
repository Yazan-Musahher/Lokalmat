import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useCart } from '../../../contexts/CartContext';

const CartModal = ({ isOpen, closeCart }) => {
  const navigate = useNavigate();
  const { cartItems, removeFromCart } = useCart();

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
              <button onClick={() => removeFromCart(item.id)} className="text-white bg-red-500 hover:bg-red-700 px-3 py-1 rounded">Fjerne</button>
            </div>
          ))}
        </div>
        <div className="flex justify-end space-x-4 mt-4">
          <button onClick={closeCart} className="text-gray-700 bg-gray-200 hover:bg-gray-300 px-4 py-2 rounded shadow">Lukk</button>
          <button onClick={() => navigate('/payment')} className="text-white bg-green-600 hover:bg-green-700 px-4 py-2 rounded shadow">Gå til betaling</button>
        </div>
      </div>
    </div>
  );
};

export default CartModal;
