// src/userModules/HomeModulesAuth/ProtectedRoute.js
import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth } from '../../contexts/AuthContext';

const ProtectedRoute = ({ element: Component, ...rest }) => {
    const { isAuthenticated } = useAuth();
    console.log("Is Authenticated:", isAuthenticated); // Debugging line

    if (!isAuthenticated) {
        console.log("Redirecting to login..."); // Debugging line
        return <Navigate to="/login/" />;
    }

    return <Component {...rest} />;
};

export default ProtectedRoute;
