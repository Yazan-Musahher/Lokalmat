import React from 'react';
import { Link } from 'react-scroll';

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
            <a href="/login/" className="login-link" onClick={closeNav}>
              Log inn
            </a>
          </div>
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
      `}</style>
    </>
  );
};

export default NavMobile;