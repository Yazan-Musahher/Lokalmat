import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { AiFillFacebook, AiFillGoogleCircle } from 'react-icons/ai';
import Navbar from './navbar';

const Signup = () => {
    const navigate = useNavigate();

    const [signupData, setSignupData] = useState({
        email: '',
        password: '',
        name: '',
        lastName: '',
        address: '',
        phone: '',
        userType: ''
    });

    const [signupSuccess, setSignupSuccess] = useState('');
    const [signupError, setSignupError] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setSignupData(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const signupUrl = `${process.env.REACT_APP_API_BASE_URL}${process.env.REACT_APP_AUTH_SIGNUP_URL}`;
        try {
            const response = await fetch(signupUrl, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(signupData)
            });
    
            if (response.ok) {
                setSignupSuccess('Registrering vellykket. Omdirigerer til innlogging...');
                setTimeout(() => navigate('/login/'), 3000);
            } else {
                const resJson = await response.json();
                setSignupError(resJson.message || 'Signup failed');
            }
        } catch (error) {
            console.error('An error occurred:', error);
            setSignupError('An error occurred during signup. Please try again.');
        }
    };
    

    return (
        <>
            <Navbar />
            <div className="min-h-screen flex flex-col items-center justify-center px-4 sm:px-6 lg:px-8">
                <div className="max-w-md w-full space-y-8">
                    <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">Opprette konto</h2>
                    {signupSuccess && <div className="text-center my-4 p-3 bg-green-200 text-green-800 rounded">{signupSuccess}</div>}
                    {signupError && <div className="text-red-500 text-center mb-4">{signupError}</div>}
                    <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
                        <div>
                            <label htmlFor="name" className="sr-only">Navn</label>
                            <input id="name" name="name" type="text" autoComplete="name" required className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm" placeholder="Navnet ditt" value={signupData.name} onChange={handleChange} />
                        </div>
                        <div>
                            <label htmlFor="email" className="sr-only">Epost</label>
                            <input id="email" name="email" type="email" autoComplete="email" required className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm" placeholder="ole@example.com" value={signupData.email} onChange={handleChange} />
                        </div>
                        <div>
                            <label htmlFor="address" className="sr-only">Adresse</label>
                            <input id="address" name="address" type="text" autoComplete="address" required className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm" placeholder="din adresse" value={signupData.address} onChange={handleChange} />
                        </div>
                        <div>
                            <label htmlFor="phone" className="sr-only">Telefon nummer</label>
                            <input id="phone" name="phone" type="tel" autoComplete="tel" required className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm" placeholder="Mobil nummeret ditt" value={signupData.phone} onChange={handleChange} />
                        </div>
                        <div>
                            <label htmlFor="password" className="sr-only">Passord</label>
                            <input id="password" name="password" type="password" autoComplete="new-password" required className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm" placeholder="********" value={signupData.password} onChange={handleChange} />
                        </div>
                        <div>
                            <label className="mr-2">Bruker type</label>
                            <select name="userType" value={signupData.userType} onChange={handleChange} required className="block w-full mt-1">
                                <option value="">Velg...</option>
                                <option value="PrivateUser">Privat</option>
                                <option value="Manufacturer">Produsent</option>
                                <option value="LargeHousehold">Storhusholdning</option>
                            </select>
                        </div>
                        <div>
                            <button type="submit" className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-green-600 hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500">
                                Opprett konto
                            </button>
                        </div>
                    </form>
                    <div className="mt-6 text-center">
                        <p className="text-sm">Har du bruker fra før?{' '}
                            <a href="/login/" className="font-medium text-green-600 hover:text-green-500">
                                Logg inn
                            </a>
                        </p>
                        <div className="relative mt-6">
                            <div className="absolute inset-0 flex items-center">
                                <div className="w-full border-t border-gray-300"></div>
                            </div>
                            <div className="relative flex justify-center text-sm">
                                <span className="px-2 bg-white text-gray-500">
                                    Eller du kan opprette en konto med
                                </span>
                            </div>
                        </div>
                        <div className="mt-6 grid grid-cols-2 gap-3">
                            <div>
                                <button type="button" className="w-full inline-flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700">
                                    <AiFillFacebook className="h-5 w-5 mr-2" />
                                    Facebook
                                </button>
                            </div>
                            <div>
                                <button type="button" className="w-full inline-flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-red-600 hover:bg-red-700">
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

export default Signup;