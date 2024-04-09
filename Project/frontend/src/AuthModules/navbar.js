import React from 'react';
import { useLocation } from 'react-router-dom';

const Navbar = () => {
  const location = useLocation();
  
  const isLogin = location.pathname === '/login/';
  const linkLabel = isLogin ? 'Opprett konto' : 'Logg inn';
  const linkPath = isLogin ? '/signup/' : '/login/';

  return (
    <nav className="bg-white shadow-md fixed top-0 left-0 w-full z-10">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between h-16 items-center">
          <a href="/" className="flex-shrink-0 flex items-center no-underline text-green-600 mr-auto">
            <span className="font-bold text-5xl">Lokalmat</span>
          </a>
          <div className="flex items-center space-x-16">
            <a href="/news" className="text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Nyheter
            </a>
            <a href="/producers" className="text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Produsenter
            </a>
            <a href="/tips" className="text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Ukens tips
            </a>
            <a href="/products" className="text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Produkter
            </a>
            <a href="/about" className="text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Om oss
            </a>
            <a
              href={linkPath}
              className="inline-flex items-center px-4 py-2 border border-transparent no-underline text-sm font-medium rounded-md text-white bg-green-500 hover:bg-green-600 focus:outline-none focus:border-green-700 focus:ring focus:ring-green-200 active:bg-green-700"
            >
              {linkLabel}
            </a>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
