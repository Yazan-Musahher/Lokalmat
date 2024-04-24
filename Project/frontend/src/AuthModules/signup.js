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
        try {
            const response = await fetch('http://localhost:5176/auth/signup', {
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

    const userTypeDisplayMapping = {
        'PrivateUser': 'Privat',
        'Manufacturer': 'Produsent',
        'LargeHousehold': 'Storhusholdning'
    };

const renderUserTypeSpecificFields = () => {
    const commonFields = (
        <>
            <div className="relative mb-6" data-twe-input-wrapper-init>
            <label class="text-sm mb-2 block">Email</label>
                <input
                    id="email"
                    name="email"
                    type="email"
                    autoComplete="email"
                    required
                    className="w-full text-sm px-4 py-3.5 rounded-md outline-none border-2  focus:border-green-500"
                    placeholder="ole@example.com"
                    value={signupData.email}
                    onChange={handleChange}
                />
            </div>
            <div className="relative mb-6" data-twe-input-wrapper-init>
            <label class="text-sm mb-2 block">Passord</label>
                <input
                    id="password"
                    name="password"
                    type="password"
                    autoComplete="new-password"
                    required
                    className="w-full text-sm px-4 py-3.5 rounded-md outline-none border-2  focus:border-green-500"
                    placeholder="********"
                    value={signupData.password}
                    onChange={handleChange}
                />
            </div>
            <div className="relative mb-6" data-twe-input-wrapper-init>
            <label class="text-sm mb-2 block">Adresse</label>
                <input
                    id="address"
                    name="address"
                    type="text"
                    autoComplete="address"
                    required
                    className="w-full text-sm px-4 py-3.5 rounded-md outline-none border-2  focus:border-green-500"
                    placeholder="din adresse"
                    value={signupData.address}
                    onChange={handleChange}
                />
            </div>
            <div className="relative mb-6" data-twe-input-wrapper-init>
            <label class="text-sm mb-2 block">Mobile Nummer</label>
                <input
                    id="phone"
                    name="phone"
                    type="tel"
                    autoComplete="tel"
                    required
                    className="w-full text-sm px-4 py-3.5 rounded-md outline-none border-2  focus:border-green-500"
                    placeholder="Mobil nummeret ditt"
                    value={signupData.phone}
                    onChange={handleChange}
                />
            </div>
        </>
    );

    if (signupData.userType === 'PrivateUser') {
        return (
            <>
            <div class="grid sm:grid-cols-2 gap-y-7 gap-x-12">
                <div className="relative mb-6" data-twe-input-wrapper-init>
                <label class="text-sm mb-2 block">Navn</label>
                    <input
                        id="name"
                        name="name"
                        type="text"
                        autoComplete="name"
                        required
                        className="w-full text-sm px-4 py-3.5 rounded-md outline-none border-2  focus:border-green-500"
                        placeholder="Navnet ditt"
                        value={signupData.name}
                        onChange={handleChange}
                    />
                </div>
                <div className="relative mb-6" data-twe-input-wrapper-init>
                <label class="text-sm mb-2 block">Etter Navn</label>
                    <input
                        id="lastName"
                        name="lastName"
                        type="text"
                        autoComplete="lastName"
                        required
                        className="w-full text-sm px-4 py-3.5 rounded-md outline-none border-2  focus:border-green-500"
                        placeholder="Etter navn"
                        value={signupData.lastName}
                        onChange={handleChange}
                    />
                </div>
                {commonFields}
                </div>
            </>
        );
    } else if (signupData.userType === 'Manufacturer') {
        return (
            <>
                <div className="relative mb-6" data-twe-input-wrapper-init>
                <label class="text-sm mb-2 block">Bedrift Navn</label>
                    <input
                        id="companyName"
                        name="name"
                        type="text"
                        autoComplete="name"
                        required
                        className="w-full text-sm px-4 py-3.5 rounded-md outline-none border-2  focus:border-green-500"
                        placeholder="Bedrift As"
                        value={signupData.name}
                        onChange={handleChange}
                    />
                </div>
                {commonFields}
                
            </>
        );
    } else if (signupData.userType === 'LargeHousehold') {
        return (
            <>
<div className="relative mb-6" data-twe-input-wrapper-init>
<label class="text-sm mb-2 block">Storhusholdning Navn</label>
    <input
        id="name"
        name="name"
        type="text"
        autoComplete="organization"
        required
        className="w-full text-sm px-4 py-3.5 rounded-md outline-none border-2  focus:border-green-500"
        placeholder="Storholsholdning navn"
        value={signupData.name}
        onChange={handleChange}
    />
</div>

                {commonFields}
            </>
        );
    }

    return null;
};
    

    return (
        <>
            <Navbar />
            <div className="min-h-screen flex flex-col items-center justify-center px-4 sm:px-6 lg:px-8 mt-10">
                <div className="max-w-md w-full space-y-8">
                    <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">Opprette konto</h2>
                    {signupSuccess && <div className="text-center my-4 p-3 bg-green-200 text-green-800 rounded">{signupSuccess}</div>}
                    {signupError && <div className="text-red-500 text-center mb-4">{signupError}</div>}
                    <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
                        <div className="space-y-2">
                            <span className="text-gray-700">Velg en bruker type:</span>
                            <div className="flex justify-center space-x-4">
                            {Object.entries(userTypeDisplayMapping).map(([value, label]) => (
                                    <button
                                        key={value}
                                        type="button"
                                        onClick={() => setSignupData({ ...signupData, userType: value })}
                                        className={`px-4 py-2 rounded-lg text-sm font-medium shadow-sm transition-colors ${
                                            signupData.userType === value ? 'bg-green-500 text-white' : 'bg-gray-200 text-gray-800'
                                        }`}
                                    >
                                        {label}
                                    </button>
                                ))}
                            </div>
                        </div>
                        {renderUserTypeSpecificFields()}
                        <button type="submit" className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-green-600 hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500">
                            Opprett konto
                        </button>
                    </form>
                    <div className="mt-6">
                        <p className="text-sm text-center">Har du bruker fra før?{' '}
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
                            <button type="button" className="w-full inline-flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700">
                                <AiFillFacebook className="h-5 w-5 mr-2" /> Facebook
                            </button>
                            <button type="button" className="w-full inline-flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-red-600 hover:bg-red-700">
                                <AiFillGoogleCircle className="h-5 w-5 mr-2" /> Google
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default Signup;
