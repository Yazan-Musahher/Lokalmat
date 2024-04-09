import logo from './logo.svg';
import './App.css';
import {BrowserRouter as Router, Routes, Route, Link, Navigate} from 'react-router-dom';

//AdminModules
import SomeAdminPage from './adminModules/Shared/Components/SomeAdminPage';
import Header from "./adminModules/Shared/Components/Header/Header";
import SharedLayout from "./adminModules/Shared/SharedLayout";
import Sidebar from "./adminModules/Shared/Components/Sidebar/Sidebar";

import Dashboard from "./adminModules/Dashboard/Dashboard";
import Products from "./adminModules/Products/Products";
import ProductPage from "./adminModules/Storage/ProductPage";

import OrderPage from "./adminModules/Orders/OrderPage";
import ProducerProfilePage from "./adminModules/ProducerProfilePage";

import AdminModule from './adminModules';
import StockComponent from "./adminModules/Sales/Components/StockComponent";
import OrderComponent from "./adminModules/Sales/Components/OrderComponent";
import TransporterComponent from "./adminModules/Sales/Components/TransporterComponent";
import AdminControlComponent from "./adminModules/Sales/Components/AdminControlComponent";

//AuthModules
import Login from './AuthModules/login';
import Signup from './AuthModules/signup'
import TransporterProfilePage from "./adminModules/TransporterProfilePage";

//HomeModules
import Home from './userModules/HomeModules/Home/Home';



function App() {
    return (
        <Router>
            <Routes>
                {/*Routing for HomeModules*/}
                <Route path="/" element={<Home />} />
                {/*Routing for AuthModules*/}
                <Route path="/login/" element={<Login />} />
                <Route path="/signup/" element={<Signup />} />
            
                 {/*Routing for AdminModule*/}
                <Route path="/admin" element={<SharedLayout />}>
                    {/* AdminModule is the default child route of "/admin" */}
                    <Route index element={<Navigate replace to="dashboard" />} />
                    <Route path="dashboard" element={<Dashboard />} />


                    <Route path="stock" element={<StockComponent />} />
                    <Route path="stock/product-page" element={<ProductPage />} />
                    <Route path="stock/product-page/:productId" element={<ProductPage />} />

                    <Route path="products" element={<Products />} />

                    <Route path="order" element={<OrderComponent />} />
                    <Route path="order/order-page" element={<OrderPage />} />
                    <Route path="order/order-page/:orderId" element={<OrderPage />} />


                    <Route path="transporter" element={<TransporterComponent />} />
                    <Route path="transporter/transporter-page" element={<TransporterProfilePage />} />
                    <Route path="transporter/transporter-page/:transporterId" element={<TransporterProfilePage />} />


                    <Route path="producer-page" element={<ProducerProfilePage />} />


                    <Route path="some-admin-page" element={<SomeAdminPage />} />

                    {/* ... other nested admin routes ... */}

                    <Route path="/admin/stock" element={<StockComponent/>}/>
                    <Route path="/admin/order" element={<OrderComponent/>}/>
                    <Route path="/admin/transporter" element={<TransporterComponent/>}/>
                    <Route path="/admin/adminOverview" element={<AdminControlComponent/>}/>

                </Route>
                <Route path="/admin" element={<AdminModule />} />
                <Route path="/admin/some-admin-page" element={<SomeAdminPage />} />
               
                
               
                <Route path="/admin/test-sharedlayout" element={<SharedLayout />}  />
                <Route path="/admin/test-header" element={<Header />}  />
                <Route path="/admin/test-sidebar" element={<Sidebar />}  />
                <Route path="/admin" element={<AdminModule />} />
            </Routes>
        </Router>
    );

}

export default App;
