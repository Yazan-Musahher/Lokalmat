import React, { useState, useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import { AiFillFacebook, AiFillGoogleCircle } from 'react-icons/ai';
import Navbar from './navbar';
import {API_BASE_URL, AUTH_LOGIN_URL} from '../credentials';
const Login = () => {
    const navigate = useNavigate();
    const { login } = useAuth();
    const [loginData, setLoginData] = useState({ email: '', password: '' });
    const [loginError, setLoginError] = useState('');

    useEffect(() => {
        if (localStorage.getItem('token')) {
            navigate('/HomeAuth');
        }
    }, [navigate]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setLoginData(prev => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const loginUrl = `${API_BASE_URL}${AUTH_LOGIN_URL}`;
        console.log("REQUEST: ", loginUrl)
        try {
            const response = await fetch(loginUrl, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(loginData),
            });

            const data = await response.json();
            if (response.ok) {
                login(data.token, {
                    email: loginData.email,
                    name: data.name,
                    role: data.role,
                    userId: data.userId,
                });
                const redirectTo = data.role === 'PrivateUser' ? '/HomeAuth' : '/produsent';
                navigate(redirectTo, { replace: true });
            } else {
                setLoginError('Login failed. Please check your credentials.');
            }
        } catch (error) {
            console.error('An error occurred:', error);
            setLoginError('An error occurred during login. Please try again.');
        }
        
    };
    
    return (
        <>
            <Navbar />
            <div className="min-h-screen flex flex-col items-center justify-center  px-4 sm:px-6 lg:px-8">
                <div className="max-w-md w-full">
                    <h2 className="mb-6 text-3xl text-center text-gray-900">Velkommen</h2>
                    {loginError && <div className="text-red-500 text-center mb-4">{loginError}</div>}
                    <form className="space-y-4" onSubmit={handleSubmit}>
                        <div>
                            <label htmlFor="email" className="sr-only">Epost</label>
                            <input 
                                id="email" 
                                name="email"
                                type="email" 
                                required 
                                className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm" 
                                placeholder="Epost" 
                                value={loginData.email}
                                onChange={handleChange}
                            />
                        </div>
                        <div>
                            <label htmlFor="password" className="sr-only">Password</label>
                            <input 
                                id="password" 
                                name="password"
                                type="password" 
                                required 
                                className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm" 
                                placeholder="Passord" 
                                value={loginData.password}
                                onChange={handleChange}
                            />
                        </div>
                        <div>
                            <button type="submit" className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md  text-white bg-green-600 hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
                                Log inn
                            </button>
                        </div>
                    </form>
                    <div className="mt-6 flex justify-center">
                        <div className="text-sm">
                            <a href="/Password-request/" className="font-medium text-indigo-600 hover:text-indigo-500">
                                Glemte passord?
                            </a>
                        </div>
                    </div>
                    <div className="mt-6">
                        <div className="relative">
                            <div className="absolute inset-0 flex items-center">
                                <div className="w-full border-t border-gray-300"></div>
                            </div>
                            <div className="relative flex justify-center text-sm">
                                <span className="px-2 bg-white text-gray-500">
                                    Eller du kan logge deg inn med 
                                </span>
                            </div>
                        </div>
                        <div className="mt-6 grid grid-cols-2 gap-3">
                            <div>
                            <button
                  type="button"
                  className="w-full inline-flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700"
                >
                  <AiFillFacebook className="h-5 w-5 mr-2" />
                  Facebook
                </button>
                            </div>
                            <div>
                            <button
                  type="button"
                  className="w-full inline-flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-red-600 hover:bg-red-700"
                >
                  <AiFillGoogleCircle className="h-5 w-5 mr-2" />
                  Google
                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default Login;
