import React from 'react';
import { useNavigate } from 'react-router-dom';

const PaymentDone = () => {
    const navigate = useNavigate();

    return (
        <div className="flex items-center justify-center min-h-screen bg-green-50">
            <div className="text-center">
                <div className="mb-4">
                    <svg className="mx-auto mb-4 w-16 h-16 text-green-600" fill="none" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" viewBox="0 0 24 24" stroke="currentColor">
                        <path d="M5 13l4 4L19 7"></path>
                    </svg>
                    <h2 className="text-2xl font-bold text-green-600 mb-2">Betaling Utført!</h2>
                    <p className="text-gray-600">Din betaling har blitt behandlet vellykket.</p>
                </div>
                <button onClick={() => navigate('/HomeAuth')}
                        className="px-6 py-2 text-white bg-green-600 hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-600 focus:ring-opacity-50 rounded-md shadow-sm">
                    Gå tilbake
                </button>
            </div>
        </div>
    );
};

export default PaymentDone;
