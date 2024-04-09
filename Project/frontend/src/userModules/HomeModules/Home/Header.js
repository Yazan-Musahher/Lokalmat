import React from 'react';
import { useNavigate } from 'react-router-dom';

const Header = () => {
    const navigate = useNavigate();
    //navigate to signup 
    const handleSignUpClick = () => {
        navigate('/signup/');
    };
    

    
    return (
        <div style={{
          background: "linear-gradient(90deg, #ffe6e6, #f5fffe 26.81%, #fcf3ff 54.58%, #fff4e6 99.86%)",
          minHeight: "100vh",
        }} className="flex flex-col">
            <div className="flex-1 flex flex-col justify-center items-start pl-80">
                <h1 className="text-6xl font-bold text-gray-800 mb-6 leading-none">
                    LOKALMAT I DITT
                </h1>
                <h1 className="text-6xl text-custom-brown mb-8">
                    NÆROMRÅDE
                </h1>
                <p className="text-2xl text-gray-700 mb-8">
                    Vær med å støtte ditt lokale næringsliv
                </p>    
                <p className="text-lg text-gray-600 mb-10">
                    Hvis du leter etter en oversiktlig plattform for både kjøp 
                    <p>og salg av lokele råvarer er du kommet til den rette plassen!</p>
                </p>
                <div className="flex gap-4">
                    <button onClick={handleSignUpClick} className="bg-green-700 text-white font-bold py-2 px-4 rounded-full">
                        Kom i gang
                    </button>
                    <button className="bg-green-700 text-white font-semibold py-2 px-4 border border-green-500 hover:border-transparent rounded-full flex items-center">
                        <span className="inline-block mr-2">▶</span> Se video
                    </button>
                </div>
            </div>
        </div>
    );
};

export default Header;
