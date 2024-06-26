import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './contexts/AuthContext';
import { CartProvider } from './contexts/CartContext';
import ProtectedRoute from './userModules/HomeModulesAuth/ProtectedRoute';

// AdminModules
import SomeAdminPage from './adminModules/Shared/Components/SomeAdminPage';
import Header from './adminModules/Shared/Components/Header/Header';
import SharedLayout from './adminModules/Shared/SharedLayout';
import Sidebar from './adminModules/Shared/Components/Sidebar/Sidebar';
import Dashboard from './adminModules/Dashboard/Dashboard';
import Products from './adminModules/Products/Products';
import ProductPage from './adminModules/Storage/ProductPage';
import OrderPage from './adminModules/Orders/OrderPage';
import ProducerProfilePage from './adminModules/ProducerProfilePage';
import StockComponent from './adminModules/Sales/Components/StockComponent';
import OrderComponent from './adminModules/Sales/Components/OrderComponent';
import TransporterComponent from './adminModules/Sales/Components/TransporterComponent';
import AdminControlComponent from './adminModules/Sales/Components/AdminControlComponent';
import TransporterProfilePage from './adminModules/TransporterProfilePage';

// AuthModules
import Login from './AuthModules/login';
import Signup from './AuthModules/signup';
import PwdRequest from './AuthModules/pwdrequest';
import PwdReset from './AuthModules/pwdreset';


// HomeModules
import Home from './userModules/HomeModules/Home/Home';

// HomeModulesAuth
import HomeAuth from './userModules/HomeModulesAuth/Home/HomeAuth';
import PaymentDone from './userModules/HomeModulesAuth/Home/PaymentDone';
import OrderHistoric from './userModules/HomeModulesAuth/Home/OrderHistoric';

// ProfileModules
import Profile from './userModules/ProfileModules/Profile';




function App() {
    return (
        <AuthProvider>
            <Router>
                <Routes>
                    {/* Home Modules */}
                    <Route path="/" element={<Home />} />
                    <Route path="/login/" element={<Login />} />
                    <Route path="/signup/" element={<Signup />} />
                    <Route path="/Password-request/" element={<PwdRequest />} />
                    <Route path="/Password-Reset/" element={<PwdReset />} />
                    <Route path="/order-success/" element={<PaymentDone />} />


                    {/* Protected Home Module */}
                    <Route path="/HomeAuth" element={<ProtectedRoute element={HomeAuth} />} />
                    <Route path="/Profile" element={<ProtectedRoute element={Profile} />} />
                    <Route path="/Order-History/" element={<ProtectedRoute element={OrderHistoric} />} />

                    {/* Admin Module Routes */}
                    <Route path="/produsent" element={<SharedLayout />}>
                        <Route index element={<Navigate replace to="dashboard" />} />
                        <Route path="dashboard" element={<Dashboard />} />
                        <Route path="stock" element={<StockComponent />} />
                        <Route path="stock/product-page" element={<ProductPage />} />
                        <Route path="stock/product-page/:productId" element={<ProductPage />} />
                        <Route path="products" element={<Products />} />
                        <Route path="order" element={<OrderComponent />} />
                        <Route path="order/order-page/:orderId" element={<OrderPage />} />
                        <Route path="transporter" element={<TransporterComponent />} />
                        <Route path="transporter/transporter-page/:transporterId" element={<TransporterProfilePage />} />
                        <Route path="producer-page" element={<ProducerProfilePage />} />
                        <Route path="adminOverview" element={<AdminControlComponent />} />
                        <Route path="some-admin-page" element={<SomeAdminPage />} />
                    </Route>

                    {/* Test Routes */}
                    <Route path="/admin/test-sharedlayout" element={<SharedLayout />} />
                    <Route path="/admin/test-header" element={<Header />} />
                    <Route path="/admin/test-sidebar" element={<Sidebar />} />
                </Routes>
            </Router>
        </AuthProvider>
    );
}

export default App;