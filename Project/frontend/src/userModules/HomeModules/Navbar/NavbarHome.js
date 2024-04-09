import React, { useState, useEffect } from 'react';
import { Link } from 'react-scroll';

const NavbarHome = () => {
  const [showNavbar, setShowNavbar] = useState(true);
  const [isNavExpanded, setIsNavExpanded] = useState(false); // State for mobile menu toggle
  let lastScrollY = window.scrollY;

  useEffect(() => {
    const handleScroll = () => {
      if (lastScrollY < window.scrollY) {
        setShowNavbar(false);
      } else {
        setShowNavbar(true);
      }
      lastScrollY = window.scrollY;
    };

    window.addEventListener('scroll', handleScroll, { passive: true });

    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, []);


    // Function to handle smooth scrolling to the section with specified id
    const handleScrollToElement = (e, id) => {
      e.preventDefault(); // Prevent the default anchor link behavior
      const element = document.getElementById(id);
      element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    };

  return (
    <nav
      className={`${
        showNavbar ? 'top-0' : '-top-full'
      } fixed left-0 w-full z-10 transition-transform duration-300`}
    >
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between h-16 items-center">
          <a href="/" className="flex-shrink-0 flex items-center no-underline text-green-600 mr-auto">
            <span className="font-bold text-xl sm:text-5xl">Lokalmat</span>
          </a>
          <button
            className="inline-flex items-center justify-center p-2 rounded-md text-gray-700 focus:outline-none sm:hidden"
            onClick={() => setIsNavExpanded(!isNavExpanded)}
          >
            {/* Hamburger Icon */}
            <svg
              className="h-6 w-6"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth="2"
                d="M4 6h16M4 12h16m-7 6h7"
              />
            </svg>
          </button>
          <div
            className={`${
              isNavExpanded ? 'flex' : 'hidden'
            } flex-col sm:flex sm:flex-row sm:justify-end space-y-4 sm:space-y-0 sm:space-x-16`}
          >
            {/* Navigation Links */}
            <Link to="news" smooth={true} duration={1500} className=" cursor-pointer text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
             Nyheter
             </Link>
             <Link to="producers" smooth={true} duration={1500} className=" cursor-pointer text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
             Produsenter
            </Link>
            <a href="#tips" className="text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Ukens tips
            </a>
            <Link to="products" smooth={true} duration={1500} className=" cursor-pointer text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
            Produkter
            </Link>
            <a href="#about" onClick={(e) => handleScrollToElement(e, 'about')} className="text-gray-500 hover:text-gray-700 no-underline inline-flex items-center px-1 pt-1 text-sm font-medium">
              Om oss
            </a>
            <a
              href="/contact"
              className="inline-flex items-center px-4 py-2 border border-transparent no-underline text-sm font-medium rounded-full text-white bg-green-900 hover:bg-green-600 focus:outline-none focus:border-green-700 focus:ring focus:ring-green-200 active:bg-green-700"
            >
              Log inn
            </a>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default NavbarHome;
