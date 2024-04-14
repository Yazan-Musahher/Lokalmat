import React, { useState } from 'react';
import Navbar from "./navbar";
import { useSearchParams } from 'react-router-dom';

const PwdReset = () => {
    const [searchParams] = useSearchParams();
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [message, setMessage] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (password !== confirmPassword) {
            setMessage("Passwords do not match.");
            return;
        }

        const email = searchParams.get('email');
        const token = searchParams.get('token');

        const response = await fetch('http://localhost:5176/Auth/resetPassword', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email, token, newPassword: password }),
        });

        const data = await response.json();

        if (response.ok) {
            setMessage('Your password has been successfully reset.');
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
                            <div>
                                <label htmlFor="password" className="text-sm font-bold text-gray-600 block">New Password</label>
                                <input
                                    type="password"
                                    className="w-full p-2 border border-gray-300 rounded mt-1"
                                    id="password"
                                    required
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                />
                            </div>
                            <div>
                                <label htmlFor="confirmPassword" className="text-sm font-bold text-gray-600 block">Confirm New Password</label>
                                <input
                                    type="password"
                                    className="w-full p-2 border border-gray-300 rounded mt-1"
                                    id="confirmPassword"
                                    required
                                    value={confirmPassword}
                                    onChange={(e) => setConfirmPassword(e.target.value)}
                                />
                            </div>
                            <button
                                type="submit"
                                className="w-full py-2 px-4 bg-green-500 hover:bg-green-700 rounded-md text-white text-sm"
                            >
                                Reset Password
                            </button>
                        </form>
                    </div>
                </div>
            </div>
        </>
    );
};

export default PwdReset;
