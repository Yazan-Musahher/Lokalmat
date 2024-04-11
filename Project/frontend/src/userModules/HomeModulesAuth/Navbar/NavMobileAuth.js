import React from 'react';
import { Link } from 'react-scroll';

const NavMobileAuth = ({ isNavExpanded, setIsNavExpanded }) => {
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
              User name
            </a>
      </div>
    </>
  );
};

export default NavMobileAuth;
