import React, { useEffect, useState } from 'react';
import NavbarHomeAuth from '../Navbar/NavbarHomeAuth';

const OrderHistoric = () => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const userId = localStorage.getItem('userId');
    if (!userId) {
      setError('No user ID found');
      setLoading(false);
      return;
    }

    const fetchOrders = async () => {
      try {
        const response = await fetch(`http://localhost:5176/api/Payment/order-history/${userId}`);
        if (!response.ok) {
          throw new Error(`HTTP status ${response.status}`);
        }
        const data = await response.json();
        setOrders(Array.isArray(data) ? data : []);
      } catch (err) {
        setError(`Failed to fetch orders: ${err.message}`);
      } finally {
        setLoading(false);
      }
    };

    fetchOrders();
  }, []);

  return (
    <>
      <NavbarHomeAuth/>
      <div className="container mx-auto px-4 pt-28 pb-12">
        <h1 className="text-2xl font-semibold text-green-600 mb-6 text-center">Ordre Historikk</h1>
        {loading ? (
          <div className="text-center text-md text-green-600 font-medium">Loading...</div>
        ) : error ? (
          <div className="text-center text-md text-red-600 font-medium">{error}</div>
        ) : (
          <div className="space-y-3">
            {orders.length > 0 ? orders.map((order) => (
              <div key={order.id} className="bg-white rounded-lg shadow overflow-hidden transform transition duration-300 hover:scale-105 flex flex-col md:flex-row md:items-center">
                <div className="p-2 flex-grow">
                  <h2 className="text-lg font-bold text-green-700">Ordre Nummer: {order.id}</h2>
                  <p className="text-sm text-gray-600">Bestillingsdato: {new Date(order.orderDate).toLocaleDateString()}</p>
                </div>
                <div className="p-2 md:p-2 border-t md:border-t-0 md:border-l">
                  <h3 className="text-md font-semibold text-gray-800">Varer:</h3>
                  {order.items.map((item) => (
                    <div key={item.productId} className="flex items-center mt-1">
                      <img src={item.imageUrl} alt="Product" className="w-16 h-16 object-cover rounded-md mr-2"/> 
                      <div className="flex-grow">
                        <p className="text-xs text-gray-600"> Artikkelnummer: {item.productId}</p>
                        <p className="text-xs text-gray-600">stk: {item.quantity}</p>
                        <p className="text-xs text-green-600">Enhetspris: NOK {item.unitPrice.toFixed(2)}</p>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            )) : <div className="text-center text-md text-gray-600 font-medium">No orders found.</div>}
          </div>
        )}
      </div>
    </>
  );
}

export default OrderHistoric;
