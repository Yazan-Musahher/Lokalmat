import React, { useState} from 'react';
import { Link } from 'react-scroll';
import { useNavigate, } from 'react-router-dom';
import { useCart } from '../../../contexts/CartContext';
import CartModal from '../Home/CartModal';

const NavMobile = ({ isNavExpanded, setIsNavExpanded }) => {
  const closeNav = () => {
    setIsNavExpanded(false);
  };

  const closeIcon = (
    <svg className="h-6 w-6" fill="none" stroke="white" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
    </svg>
  );

  const hamburgerIcon = (
    <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16m-7 6h7" />
    </svg>
  );

  const navigate = useNavigate();

  const userEmail = localStorage.getItem('name');
  const { cartItems } = useCart();
  const itemCount = cartItems.reduce((total, item) => total + item.quantity, 0);
  const [isCartOpen, setIsCartOpen] = useState(false);


  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    localStorage.removeItem('name');
    localStorage.removeItem('role');
    navigate('/login/');
  };

  return (
    <>
      {isNavExpanded && (
        <div className="fixed inset-0 bg-black bg-opacity-75 backdrop-blur-sm z-50 flex items-center justify-center sm:hidden">
          <button
            className="absolute top-5 right-5 z-60 inline-flex items-center justify-center p-2 rounded-md text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-opacity-50"
            onClick={() => setIsNavExpanded(!isNavExpanded)}
            aria-expanded={isNavExpanded}
          >
            {closeIcon}
          </button>
          <div className="flex flex-col items-center justify-center h-full ">
            <a href="/contact" className="nav-link cursor-pointer" onClick={closeNav}>
              Ukens tips
            </a>
            <Link to="products" smooth={true} duration={500} className="nav-link cursor-pointer" onClick={closeNav}>
              Produkter
            </Link>
            <Link to="news" smooth={true} duration={500} className="nav-link cursor-pointer" onClick={closeNav}>
              Nyheter
            </Link>
            <Link to="producers" smooth={true} duration={500} className="nav-link cursor-pointer" onClick={closeNav}>
              Produsenter
            </Link>
            <a href="/Order-History/" smooth={true} duration={500} className="nav-link cursor-pointer" onClick={closeNav}>
                Order Historikk
              </a>
              <a href="/Profile" className=" inline-flex items-center px-4 py-2 border border-transparent no-underline text-sm font-medium rounded-full text-white bg-green-900 hover:bg-green-600 focus:outline-none focus:border-green-700 focus:ring focus:ring-green-200 active:bg-green-700">
               {userEmail || 'User Name'}
              </a>
              <button onClick={() => setIsCartOpen(true)} className="relative flex items-center mt-6">
                <svg className="w-6 h-6" fill="none" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" viewBox="0 0 24 24" stroke="currentColor">
                  <path d="M3 3h18l-2 14H5L3 3z"></path>
                  <path d="M16 18a2 2 0 100 4 2 2 0 000-4zM8 18a2 2 0 100 4 2 2 0 000-4z"></path>
                </svg>
                <span className="absolute top-0 right-0 text-green px-1 py-0.5 text-xxs font-bold mr-6 mt-0.5">{itemCount}</span>
              </button>
              <button onClick={handleLogout} className="nav-link logout-button inline-flex items-center px-4 py-2 border border-transparent no-underline text-sm font-medium rounded-full text-white bg-red-600 hover:bg-red-700 focus:outline-none focus:border-red-700 focus:ring focus:ring-red-200 active:bg-red-700">
                Logg ut
              </button>
         
          </div>
          <CartModal isOpen={isCartOpen} closeCart={() => setIsCartOpen(false)} />
        </div>
      )}

      {!isNavExpanded && (
        <button
          className="z-60 inline-flex items-center justify-center p-2 rounded-md text-gray-700 focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-opacity-50 sm:hidden"
          onClick={() => setIsNavExpanded(!isNavExpanded)}
          aria-expanded={isNavExpanded}
        >
          {hamburgerIcon}
        </button>
      )}

      <style jsx>{`
        .nav-link {
          display: block;
          margin: 10px 0;
          color: white;
          padding: 10px;
          width: 100%;
          text-align: center;
          border-radius: 5px;
          text-decoration: none;
          transition: color 0.3s ease;
        }
        .nav-link:hover {
          color: #38a169;
        }
        .login-link {
          display: block;
          margin-top: 10px;
          padding: 10px;
          width: 100%;
          text-align: center;
          border-radius: 5px;
          background-color: #2f855a;
          color: white;
          text-decoration: none;
          border: none;
          transition: background-color 0.3s ease;
        }
        .login-link:hover {
          background-color: #276749;
        }
        .logout-button {
          background-color: #ef4444; // This is Tailwind's bg-red-600 color
          color: white; // Ensure text color is white for contrast
        }
        .logout-button:hover {
          background-color: #dc2626; // This is Tailwind's bg-red-700 color for hover state
        }
      `}</style>
    </>
  );
};

export default NavMobile;