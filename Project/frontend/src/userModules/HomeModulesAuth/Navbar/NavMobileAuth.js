import React, { useState} from 'react';
import { Link } from 'react-scroll';
import { useNavigate, } from 'react-router-dom';
import { useCart } from '../../../contexts/CartContext';


const NavMobileAuth = ({ isNavExpanded, setIsNavExpanded }) => {
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
      <button className="inline-flex items-center justify-center p-2 rounded-md text-gray-700 focus:outline-none sm:hidden" onClick={() => setIsNavExpanded(!isNavExpanded)}>
        {/* Hamburger Icon */}
        <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16m-7 6h7" />
        </svg>
      </button>
      <div className={`${isNavExpanded ? 'flex' : 'hidden'} flex-col sm:hidden space-y-4 mt-40`}>
        {/* Mobile Navigation Links */}
        <a href="/contact" className="cursor-pointer text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Ukens tips
        </a>
        <Link to="products" smooth={true} duration={500} className="cursor-pointer text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
          Produkter
        </Link>
        <Link to="news" smooth={true} duration={500} className="cursor-pointer text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
            Nyheter
        </Link>
        <Link to="producers" smooth={true} duration={500} className="cursor-pointer text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
          Produsenter
        </Link>
        <a href="/contact" className="inline-flex items-center px-4 py-2 border border-transparent no-underline text-sm font-medium rounded-full text-white bg-green-900 hover:bg-green-600 focus:outline-none focus:border-green-700 focus:ring focus:ring-green-200 active:bg-green-700">
        {userEmail || 'User Name'}
        </a>
        <button onClick={() => setIsCartOpen(true)} className="relative flex items-center">
                <svg className="w-6 h-6" fill="none" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" viewBox="0 0 24 24" stroke="currentColor">
                  <path d="M3 3h18l-2 14H5L3 3z"></path>
                  <path d="M16 18a2 2 0 100 4 2 2 0 000-4zM8 18a2 2 0 100 4 2 2 0 000-4z"></path>
                </svg>
                <span className="absolute top-0 right-0 text-green px-1 py-0.5 text-xxs font-bold mr-6 mt-0.5">{itemCount}</span>
              </button>
        <button onClick={handleLogout} className="inline-flex items-center px-4 py-2 border border-transparent no-underline text-sm font-medium rounded-full text-white bg-red-600 hover:bg-red-700 focus:outline-none focus:border-red-700 focus:ring focus:ring-red-200 active:bg-red-700">
                Log out
        </button>
      </div>
    </>
  );
};

export default NavMobileAuth;
