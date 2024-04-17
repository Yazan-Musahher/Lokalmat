import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import NavbarHomeAuth from '../HomeModulesAuth/Navbar/NavbarHomeAuth';
import {API_BASE_URL,AUTH_CURRENT_USER_URL, AUTH_UPDATE_USER_URL } from '../../credentials';

const Profile = () => {
    const navigate = useNavigate();
    const [profileData, setProfileData] = useState({
        name: '',
        lastName: '',
        address: '',
        phone: '',
        email: '',
        password: ''
    });
    const [successMessage, setSuccessMessage] = useState('');
    const [errorMessage, setErrorMessage] = useState('');

    useEffect(() => {
        const getCurrentUserData = async () => {
            try {
                const token = localStorage.getItem('token');
                if (!token) {
                    setErrorMessage('Your session has expired, please log in again.');
                    return;
                }
    
                const currentUserUrl = `${API_BASE_URL}${AUTH_CURRENT_USER_URL}`;
                const response = await fetch(currentUserUrl, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                });
                const data = await response.json();
                if (response.ok) {
                    setProfileData({
                        name: data.name || '',
                        lastName: data.lastName || '',
                        address: data.address || '',
                        phone: data.phone || '',
                        email: data.email || '',
                        password: '' // Ensure password is not pre-filled
                    });
                } else {
                    setErrorMessage('Failed to fetch user data: ' + data.message);
                }
            } catch (error) {
                setErrorMessage('Error fetching user data: ' + error.message);
            }
        };
    
        getCurrentUserData();
    }, []);
    

    const handleChange = (e) => {
        const { name, value } = e.target;
        setProfileData(prevState => ({ ...prevState, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const token = localStorage.getItem('token');
        if (!token) {
            setErrorMessage('Your session has expired, please log in again.');
            return;
        }
    
        try {
            const updateUserUrl = `${API_BASE_URL}${AUTH_UPDATE_USER_URL}`;
            const response = await fetch(updateUserUrl, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify(profileData) // Send the profile data as-is
            });
    
            const data = await response.json();
            if (response.ok) {
                setSuccessMessage('Profil redigering vellykket');
                setErrorMessage('');
            } else {
                setErrorMessage('Profil redigering mislyktes: ' + data.message);
                setSuccessMessage('');
            }
        } catch (error) {
            setErrorMessage('En feil oppstod: ' + error.message);
        }
    };
    

    


    return (
        
        <>
        <NavbarHomeAuth/>
        <div className="mt-16 min-h-screen bg-gray-100 p-8">
            <div className="container mx-auto max-w-md shadow-md hover:shadow-lg transition duration-300">
                
                {/* Success Message */}
                {successMessage && (
                        <div className="bg-green-100 border-l-4 border-green-500 text-green-700 p-4" role="alert">
                            <p className="font-bold">Success</p>
                            <p>{successMessage}</p>
                        </div>
                    )}
                    {/* Error Message */}
                    {errorMessage && (
                        <div className="bg-red-100 border-l-4 border-red-500 text-red-700 p-4" role="alert">
                            <p className="font-bold">Error</p>
                            <p>{errorMessage}</p>
                        </div>
                    )}
                
                
                <div className="py-12 p-10 bg-white rounded-xl">
                    <div className="mb-6">
                        <label htmlFor="name" className="block mb-2 text-sm text-gray-600 dark:text-gray-400">Fornavn</label>
                        <input type="text" name="name" id="name" placeholder="Ditt fornavn" onChange={handleChange} value={profileData.name} className="w-full px-3 py-2 border rounded-xl border-gray-300"/>
                    </div>
                    <div className="mb-6">
                        <label htmlFor="lastName" className="block mb-2 text-sm text-gray-600 dark:text-gray-400">Etternavn</label>
                        <input type="text" name="lastName" id="lastName" placeholder="Ditt etternavn" onChange={handleChange} value={profileData.lastName} className="w-full px-3 py-2 border rounded-xl border-gray-300"/>
                    </div>
                    <div className="mb-6">
                        <label htmlFor="address" className="block mb-2 text-sm text-gray-600 dark:text-gray-400">Adresse</label>
                        <input type="text" name="address" id="address" placeholder="Din adresse" onChange={handleChange} value={profileData.address} className="w-full px-3 py-2 border rounded-xl border-gray-300"/>
                    </div>
                    <div className="mb-6">
                        <label htmlFor="phone" className="block mb-2 text-sm text-gray-600 dark:text-gray-400">Telefonnummer</label>
                        <input type="text" name="phone" id="phone" placeholder="Ditt telefonnummer" onChange={handleChange} value={profileData.phone} className="w-full px-3 py-2 border rounded-xl border-gray-300"/>
                    </div>
                    <div className="mb-6">
                        <label htmlFor="email" className="block mb-2 text-sm text-gray-600 dark:text-gray-400">E-post</label>
                        <input type="email" name="email" id="email" placeholder="Din nåværende e-post" onChange={handleChange} value={profileData.email} className="w-full px-3 py-2 border rounded-xl border-gray-300"/>
                    </div>
                    <div className="mb-6">
                        <label htmlFor="newEmail" className="block mb-2 text-sm text-gray-600 dark:text-gray-400">Ny e-post</label>
                        <input type="email" name="newEmail" id="newEmail" placeholder="Din nye e-post" onChange={handleChange} value={profileData.newEmail} className="w-full px-3 py-2 border rounded-xl border-gray-300"/>
                    </div>
                    <div className="mb-6">
                        <label htmlFor="password" className="block mb-2 text-sm text-gray-600 dark:text-gray-400">Bekreft med ditt passord</label>
                        <input type="password" name="password" id="password" placeholder="Passord for å bekrefte endringer" onChange={handleChange} value={profileData.password} className="w-full px-3 py-2 border rounded-xl border-gray-300"/>
                    </div>
                    <div className="flex justify-between">
                        <button type="button" onClick={() => navigate(-1)} className="text-white bg-gray-500 hover:bg-gray-600 focus:ring-4 focus:outline-none focus:ring-gray-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center">Avbryt</button>
                        <button type="submit" onClick={handleSubmit} className="text-white bg-green-600 hover:bg-green-700 focus:ring-4 focus:outline-none focus:ring-green-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center">Lagre Endringer</button>
                    </div>
                </div>
            </div>
        </div>
        </>
    );
};

export default Profile;