
import React from 'react';
import './Header.css';
import {Link, useLocation} from "react-router-dom";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faSearch, faUserCircle, faBars} from "@fortawesome/free-solid-svg-icons";

function Header({ toggleSidebar }) {

    const location = useLocation();

    const screenTexts = {
        '/admin/dashboard': 'Velkommen Ann Lee - Dashboard',
        '/admin/stock': 'Velkommen Ann Lee - Lager',
        '/admin/stock/product-page': 'Velkommen Ann Lee - Legg til produkt',
        '/admin/products': 'Velkommen Ann Lee - Produkter',
        '/admin/sales': 'Velkommen Ann Lee - Salg',
        '/admin/order': 'Velkommen Ann Lee - Ordre',
        '/admin/transporter': 'Velkommen Ann Lee - Transportør',
    };

    const getScreenText = (pathname) => screenTexts[pathname] || 'Velkommen Ann Lee';


    return (
        <nav className="navbar navbar-expand-lg custom-navbar">
            <div className="container-fluid">
                {/* Hamburger menu icon for toggling sidebar */}
                <button className="navbar-toggler" type="button" onClick={toggleSidebar}>
                    <FontAwesomeIcon icon={faBars} className="navbar-toggler-icon" />
                </button>

                {/* Brand/logo */}
                <Link className="navbar-brand" to="/">
                    {getScreenText(location.pathname)}
                </Link>


                {/* Right-aligned items on larger screens */}
                <form className="d-flex" role="search">
                    <FontAwesomeIcon icon={faSearch} className="me-2 text-white" />
                    <FontAwesomeIcon icon={faUserCircle} className="text-white" />
                    <span className="ms-2 text-white">Ann Lee</span>
                </form>
            </div>
        </nav>
    );
}

export default Header;