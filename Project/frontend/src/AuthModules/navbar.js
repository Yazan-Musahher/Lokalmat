import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { FaBars, FaTimes } from 'react-icons/fa';

const Navbar = () => {
  const location = useLocation();
  const [isOpen, setIsOpen] = useState(false);
  const [isMobile, setIsMobile] = useState(window.innerWidth < 768);

  const isLogin = location.pathname === '/login/';
  const linkLabel = isLogin ? 'Opprett konto' : 'Logg inn';
  const linkPath = isLogin ? '/signup/' : '/login/';

  const toggleMenu = () => {
    setIsOpen(!isOpen);
  };

  const closeMenu = () => {
    setIsOpen(false);
  };

  // Listen to resize events and update the isMobile state accordingly
  useEffect(() => {
    function handleResize() {
      setIsMobile(window.innerWidth < 768);
    }

    window.addEventListener('resize', handleResize);
    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);

  // Mobile Navbar
  const MobileNavbar = () => (
    <div>
      {!isOpen ? (
        <button className="p-2 text-green-600 hover:text-green-700" onClick={toggleMenu}>
          <FaBars />
        </button>
      ) : (
        <div className="fixed inset-0 bg-black bg-opacity-50 backdrop-blur-sm flex justify-center items-center">
          <button className="absolute top-5 right-5 text-white hover:text-gray-300" onClick={toggleMenu}>
            <FaTimes />
          </button>
          <div className="flex flex-col items-center">
          <a href="/" className="text-white text-lg no-underline py-2 hover:text-green-700" onClick={closeMenu}>Nyheter</a>
                <a href="/" className="text-white text-lg no-underline  py-2" onClick={closeMenu}>Produsenter</a>
                <a href="/" className="text-white text-lg no-underline  py-2" onClick={closeMenu}>Ukens tips</a>
                <a href="/" className="text-white text-lg no-underline  py-2" onClick={closeMenu}>Produkter</a>
                <a href="/" className="text-white text-lg no-underline  py-2" onClick={closeMenu}>Om oss</a>
                <a href={linkPath} className="px-4 py-2 border border-transparent text-lg no-underline font-medium rounded-md text-white bg-green-500 hover:bg-green-600 focus:outline-none focus:border-green-700 focus:ring focus:ring-green-200 active:bg-green-700 py-2" onClick={closeMenu}>
                  {linkLabel}
                </a>
              </div>
        </div>
      )}
    </div>
  );

  // Default Navbar for larger screens
  return (
    <nav className="bg-white shadow-md fixed top-0 left-0 w-full z-10">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between h-16 items-center">
          <a href="/" className="flex-shrink-0 flex items-center no-underline text-green-600 mr-auto">
            <span className="font-bold text-5xl">Lokalmat</span>
          </a>
          {isMobile ? <MobileNavbar /> : (
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
          )}
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
