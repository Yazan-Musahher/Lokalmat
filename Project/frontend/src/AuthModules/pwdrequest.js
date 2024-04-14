import React, { useState } from 'react';
import Navbar from "./navbar";

const PwdRequest = () => {
    const [email, setEmail] = useState('');
    const [message, setMessage] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        
        const response = await fetch('http://localhost:5176/Auth/requestPasswordReset', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email: email }),
        });

        const data = await response.json();

        if (response.ok) {
            setMessage('Vennligst sjekk e-posten din for å tilbakestille passordet ditt.');
        } else {
            setMessage(data.message || 'An error occurred. Please try again.');
        }
    };

    return (
        <>
            <Navbar />
            <div className="min-h-screen bg-gray-100 flex flex-col justify-center">
                <div className="max-w-md w-full mx-auto">
                    <div className="text-3xl font-bold text-gray-900 mt-2 text-center">Reset Password</div>
                    <div className="bg-white p-8 border border-gray-300 mt-6 rounded-lg shadow-md">
                        {message && <div className="mb-4 text-sm font-medium text-green-600">{message}</div>}
                        <form className="space-y-6" onSubmit={handleSubmit}>
                            <label htmlFor="email" className="text-sm font-bold text-gray-600 block">Email</label>
                            <input
                                type="email"
                                className="w-full p-2 border border-gray-300 rounded mt-1"
                                id="email"
                                required
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                placeholder="Enter your email"
                            />
                            <button
                                type="submit"
                                className="w-full py-2 px-4 bg-green-500 hover:bg-blue-700 rounded-md text-white text-sm"
                            >
                                Send Reset Instructions
                            </button>
                        </form>
                    </div>
                </div>
            </div>
        </>
    );
};

export default PwdRequest;
