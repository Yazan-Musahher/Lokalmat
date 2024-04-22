
import React, { useContext} from 'react';
import { Routes, Route, Link, Outlet } from 'react-router-dom';
import SomeAdminPage from './Shared/Components/SomeAdminPage'; // Replace with your actual component

//Daniel testing av enkelt komponenter
import SharedLayout from './Shared/SharedLayout'; // Import the shared layout
import Header from './Shared/Components/Header/Header'; // Import the header
import Sidebar from './Shared/Components/Sidebar/Sidebar'; // Import the sidebar

import StockComponent from "./Sales/Components/StockComponent";
import OrderComponent from  "./Sales/Components/OrderComponent";
import AdminControlComponent from "./Sales/Components/AdminControlComponent"; 

 

function AdminModule() {
    return (
        <div>
            <h2>Hello. Oversikt over </h2>

            <p><Link to="/admin/some-admin-page">Daniel side</Link></p>

            <p><Link to="/produsent/test-sharedlayout">Test SharedLayout</Link></p>
            <p><Link to="/produsent/test-header">Test Header</Link></p>
            <p><Link to="/produsent/test-sidebar">Test Sidebar</Link></p>
            <p><Link to="/produsent/stock">Varelager</Link></p>
            <p><Link to="/produsent/order">Ordre oversikt</Link></p>
            <p><Link to="/produsent/adminOverview">Annine oversikt</Link></p>
            <Routes>
                <Route path="/some-admin-page" element={<SomeAdminPage/>}/>


                <Route path="/test-sharedlayout" element={<SharedLayout/>}/>
                <Route path="/test-header" element={<Header/>}/>
                <Route path="/test-sidebar" element={<Sidebar/>}/>
                <Route path="/stock" element={<StockComponent/>}/>
                <Route path="/order" element={<OrderComponent/>}/>
                <Route path="/adminOverview" element={<AdminControlComponent/>}/>
                {/* Define more routes specific to the admin section */}
            </Routes>

        </div>
    );
}

export default AdminModule;

