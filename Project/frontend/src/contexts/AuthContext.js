import React, { createContext, useContext, useState, useEffect } from 'react';

const AuthContext = createContext({
    isAuthenticated: false,
    user: null,
    login: () => {},
    logout: () => {}
});

export const useAuth = () => useContext(AuthContext);

const checkAuth = () => {
    return {
        isAuthenticated: !!localStorage.getItem('token'),
        user: {
            email: localStorage.getItem('email'),
            name: localStorage.getItem('name'),
            role: localStorage.getItem('role'),
        }
    };
};

export const AuthProvider = ({ children }) => {
    const [authState, setAuthState] = useState(checkAuth());

    useEffect(() => {
        const handleStorageChange = () => {
            setAuthState(checkAuth());
        };

        window.addEventListener('storage', handleStorageChange);
        return () => window.removeEventListener('storage', handleStorageChange);
    }, []);

    const login = (token, userDetails) => {
        localStorage.setItem('token', token);
        localStorage.setItem('email', userDetails.email);
        localStorage.setItem('name', userDetails.name);
        localStorage.setItem('role', userDetails.role);
        setAuthState({
            isAuthenticated: true,
            user: userDetails
        });
    };

    const logout = () => {
        localStorage.clear(); // This clears all localStorage data including token, email, name, role
        setAuthState({
            isAuthenticated: false,
            user: null
        });
    };

    return (
        <AuthContext.Provider value={{ ...authState, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};