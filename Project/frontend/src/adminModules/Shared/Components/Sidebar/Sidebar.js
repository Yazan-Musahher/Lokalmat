// src/adminModules/shared/Sidebar.js
import React, {useState} from 'react';
import {Link, NavLink, useLocation} from 'react-router-dom';
import './Sidebar.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    faBook,
    faTachometerAlt,
    faBoxes, faShoppingCart, faShippingFast, faColumns
} from '@fortawesome/free-solid-svg-icons';



function Sidebar({ isSidebarOpen }) {

    return (
        <div className={`sidebar ${isSidebarOpen ? 'open' : 'closed'}`}>
            <NavLink to="/admin/dashboard" className={({ isActive }) =>
                isActive ? "sidebar-item active-link" : "sidebar-item"
            }>
                <FontAwesomeIcon icon={faColumns} className="sidebar-icon" />
                <span>Dashboard</span>
            </NavLink>

            <NavLink to="/admin/stock"
                     className={({ isActive }) =>
                         isActive ? "sidebar-item active-link" : "sidebar-item"
                     }>
                <FontAwesomeIcon icon={faBook} className="sidebar-icon" />
                <span>Lager</span>
            </NavLink>


            <NavLink to="/admin/products" className={({ isActive }) =>
                isActive ? "sidebar-item active-link" : "sidebar-item"
            }>
                <FontAwesomeIcon icon={faBoxes} className="sidebar-icon" />
                <span>Produkter</span>
            </NavLink>

            <NavLink to="/admin/sales" className={({ isActive }) =>
                isActive ? "sidebar-item active-link" : "sidebar-item"
            }>
                <FontAwesomeIcon icon={faShoppingCart} className="sidebar-icon" />
                <span>Salg</span>
            </NavLink>

            <NavLink to="/admin/order" className={({ isActive }) =>
                isActive ? "sidebar-item active-link" : "sidebar-item"
            }>
                <FontAwesomeIcon icon={faShippingFast} className="sidebar-icon" />
                <span>Ordre</span>
            </NavLink>

            <NavLink to="/admin/transporter" className={({ isActive }) =>
                isActive ? "sidebar-item active-link" : "sidebar-item"
            }>
                <FontAwesomeIcon icon={faShippingFast} className="sidebar-icon" />
                <span>Transportør</span>
            </NavLink>
            {/* Add more links as needed */}
        </div>
    );
}

export default Sidebar;