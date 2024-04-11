import React from 'react';
import { useNavigate } from 'react-router-dom';

const Header = () => {
    const navigate = useNavigate();
    // Navigate to signup 
    const handleSignUpClick = () => {
        navigate('/signup/');
    };

    return (
        <div style={{
          background: "linear-gradient(90deg, #ffe6e6, #f5fffe 26.81%, #fcf3ff 54.58%, #fff4e6 99.86%)",
        }} className="min-h-screen flex flex-col">
            {/* Add mt-10 for mobile and remove it for larger screens using lg:mt-0 */}
            <div className="flex-1 flex flex-col justify-center items-center lg:items-start pl-4 pr-4 lg:pl-80 mt-40 lg:mt-0">
                <h1 className="text-6xl font-bold text-gray-800 mb-6 leading-none lg:text-left text-center">
                    LOKALMAT I DITT
                </h1>
                <h1 className="text-6xl text-custom-brown mb-8 lg:text-left text-center">
                    NÆROMRÅDE
                </h1>
                <p className="text-2xl text-gray-700 mb-8 lg:text-left text-center">
                    Vær med å støtte ditt lokale næringsliv
                </p>    
                <p className="text-lg text-gray-600 mb-10 lg:text-left text-center">
                    Hvis du leter etter en oversiktlig plattform for både kjøp 
                    og salg av lokele råvarer er du kommet til den rette plassen!
                </p>
                {/* Apply margin-top and full-width for small screens, default for larger screens */}
                <div className="flex gap-4 justify-center lg:justify-start lg:mt-0 mt-4 lg:w-auto w-full">
                    <button onClick={handleSignUpClick} className="bg-green-700 text-white font-bold py-2 px-4 rounded-full lg:w-auto w-full">
                        Kom i gang
                    </button>
                    <button className="bg-green-700 text-white font-semibold py-2 px-4 border border-green-500 hover:border-transparent rounded-full flex items-center lg:w-auto w-full">
                        <span className="inline-block mr-2">▶</span> Se video
                    </button>
                </div>
            </div>
        </div>
    );
};

export default Header;
