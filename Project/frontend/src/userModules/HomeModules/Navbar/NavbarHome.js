import React, { useState, useEffect, useRef } from 'react';
import { Link } from 'react-scroll';
import NavMobile from './NavMobile';  // Importing the NavMobile component

const NavbarHome = () => {
  const [showNavbar, setShowNavbar] = useState(true);
  const [isNavExpanded, setIsNavExpanded] = useState(false);
  const [isAtTop, setIsAtTop] = useState(true);
  const lastScrollY = useRef(window.scrollY);

  useEffect(() => {
    const handleScroll = () => {
      const atTop = window.scrollY < 50;
      setIsAtTop(atTop);

      if (lastScrollY.current < window.scrollY) {
        setShowNavbar(false);
      } else {
        setShowNavbar(true);
      }
      lastScrollY.current = window.scrollY;
    }

    window.addEventListener('scroll', handleScroll, { passive: true });

    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, []);

  return (
    <nav className={`${showNavbar ? 'top-0' : '-top-full'} fixed left-0 w-full z-10 transition-transform duration-300 ${!isAtTop && 'bg-white shadow-md'}`}>
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between h-16 items-center">
          <a href="/" className="flex-shrink-0 flex items-center no-underline text-green-600 mr-auto">
            <span className="font-bold text-xl sm:text-5xl">Lokalmat</span>
          </a>
          <NavMobile isNavExpanded={isNavExpanded} setIsNavExpanded={setIsNavExpanded} />
          {/* Desktop Navigation Links */}
          <div className="hidden sm:flex sm:flex-row sm:justify-end space-x-16">
            <Link to="news" smooth={true} duration={500} className="cursor-pointer text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Nyheter
            </Link>
            <Link to="producers" smooth={true} duration={500} className="cursor-pointer text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Produsenter
            </Link>
            <a href="#tips" className="text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Ukens tips
            </a>
            <Link to="products" smooth={true} duration={500} className="cursor-pointer text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Produkter
            </Link>
            <a href="#about" className="text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Om oss
            </a>
            <a href="/login/" className=" cursor-pointer inline-flex items-center px-4 py-2 border border-transparent no-underline text-sm font-medium rounded-full text-white bg-green-900 hover:bg-green-600 focus:outline-none focus:border-green-700 focus:ring focus:ring-green-200 active:bg-green-700">
              Log inn
            </a>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default NavbarHome;