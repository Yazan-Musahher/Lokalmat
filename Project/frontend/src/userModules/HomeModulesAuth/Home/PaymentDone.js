import React, { useRef, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';

const PaymentDone = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const initialized = useRef(false);

    const searchParams = new URLSearchParams(location.search);
    const sessionId = searchParams.get('session_id');

    const finalizePayment = async (sessionId) => {
        console.log("Session ID from URL:", sessionId);
        const response = await fetch(`http://localhost:5176/api/Payment/finalize-payment?session_id=${sessionId}`);
        const responseData = await response.json();
        if (!response.ok) {
            console.error('Error finalizing payment:', responseData);
            navigate('/order-failure/');
        } else {
            console.log(responseData.Message);
            setTimeout(() => {
                navigate(`/HomeAuth`);
            }, 3000);
        }
    };

    useEffect(() => {
        if (sessionId && !initialized.current) {
            initialized.current = true;  // Set the flag so this effect doesn't run again for the same sessionId
            finalizePayment(sessionId);
        }
        return () => {
            initialized.current = false;  // Reset the flag when the component unmounts if needed
        };
    }, [sessionId]);  // The effect depends on sessionId

    return (
        <div className="flex items-center justify-center min-h-screen bg-green-50">
            <div className="text-center">
                <div className="mb-4">
                    <svg className="mx-auto mb-4 w-16 h-16 text-green-600" fill="none" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" viewBox="0 0 24 24" stroke="currentColor">
                        <path d="M5 13l4 4L19 7"></path>
                    </svg>
                    <h2 className="text-2xl font-bold text-green-600 mb-2">Betaling Utført!</h2>
                    <p className="text-gray-600">Vennligst vent mens vi bekrefter betalingsdetaljene dine.</p>
                </div>
            </div>
        </div>
    );
};

export default PaymentDone;
