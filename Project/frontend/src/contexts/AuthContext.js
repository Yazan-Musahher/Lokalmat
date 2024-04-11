import React, { createContext, useContext, useState, useEffect } from 'react';

const AuthContext = createContext({
    isAuthenticated: false,
    login: () => {},
    logout: () => {}
});

export const useAuth = () => useContext(AuthContext);

const checkToken = () => !!localStorage.getItem('token');

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(checkToken());

    useEffect(() => {
        // This setup helps ensure that any changes to localStorage's token
        // outside of this app's control are also reflected.
        const handleStorageChange = () => {
            setIsAuthenticated(checkToken());
        };

        window.addEventListener('storage', handleStorageChange);

        // Clean up the event listener when the component unmounts
        return () => window.removeEventListener('storage', handleStorageChange);
    }, []);

    const login = (token) => {
        localStorage.setItem('token', token);
        setIsAuthenticated(true);
    };

    const logout = () => {
        localStorage.removeItem('token');
        setIsAuthenticated(false);
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};
