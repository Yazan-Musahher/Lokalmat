import React from 'react';
import './Header.css';
import { Link, useLocation, useNavigate } from "react-router-dom";  // Import useNavigate instead of useHistory
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch, faUserCircle, faBars, faSignOutAlt } from "@fortawesome/free-solid-svg-icons";
import { useAuth } from '../../../../contexts/AuthContext';

function Header({ toggleSidebar }) {
    const location = useLocation();
    const navigate = useNavigate();  // Use useNavigate for navigation
    const { user, logout } = useAuth();  // Destructure to get user and logout directly

    const userName = user?.name || 'Guest';  // Fallback to 'Guest' if name isn't available

    const screenTexts = {
        '/produsent/dashboard': `Velkommen ${userName} - Dashboard`,
        '/produsent/stock': `Velkommen ${userName} - Lager`,
        '/produsent/stock/product-page': `Velkommen ${userName} - Legg til produkt`,
        '/produsent/products': `Velkommen ${userName} - Produkter`,
        '/produsent/sales': `Velkommen ${userName} - Salg`,
        '/produsent/order': `Velkommen ${userName} - Ordre`,
        '/produsent/transporter': `Velkommen ${userName} - Transportør`,
    };

    const getScreenText = (pathname) => screenTexts[pathname] || `Velkommen ${userName}`;

    const handleLogout = () => {
        logout();  // Clear user session from local storage and update auth state
        navigate('/login/');  // Redirect to login page using navigate
    };

    return (
        <nav className="navbar navbar-expand-lg custom-navbar">
            <div className="container-fluid">
                <button className="navbar-toggler" type="button" onClick={toggleSidebar}>
                    <FontAwesomeIcon icon={faBars} className="navbar-toggler-icon" />
                </button>

                <Link className="navbar-brand" to="/">
                    {getScreenText(location.pathname)}
                </Link>

                <div className="d-flex align-items-center ms-auto">
                    <FontAwesomeIcon icon={faUserCircle} className="text-white" />
                    <span className="ms-2 text-white mr-4">{userName}</span>
                    <button className="ms-2 btn btn-sm btn-danger" onClick={handleLogout}>
                        <FontAwesomeIcon icon={faSignOutAlt} /> Logout
                    </button>
                </div>
            </div>
        </nav>
    );
}

export default Header;
