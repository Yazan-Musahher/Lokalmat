import React from 'react';
import { FaFacebook, FaLinkedin, FaTwitter, FaPlay, FaYoutube } from 'react-icons/fa';
import middag1 from '../../../Assets/Footer/middag1.jpeg'
import middag3 from '../../../Assets/Footer/middag3.jpeg'

const Footer = () => {
    return (
        <footer className="py-10 px-4" style={{background: "linear-gradient(90deg, #ffe6e6, #f5fffe 26.81%, #fcf3ff 54.58%, #fff4e6 99.86%)"}}>
            <div className="max-w-7xl mx-auto flex flex-col lg:flex-row justify-between">
                <div className="flex flex-col text-gray-700 mb-6 lg:mb-0">
                    <h2 className="text-5xl font-bold mb-3 text-green-700">Lokalmat</h2>
                    <p className="mb-4">
                    plattform for kjøp, salg og formidling,<br />
                    for tilbydere av lokalt produserte varer.<br />
                    med et enkelt og oversiktlig brukergrensesnitt.
                    </p>
                    <div className="flex">
                        <a href="#" className="text-orange-500 text-2xl mr-6"><FaFacebook /></a>
                        <a href="#" className="text-orange-500 text-2xl mr-6"><FaLinkedin /></a>
                        <a href="#" className="text-orange-500 text-2xl mr-6"><FaTwitter /></a>
                        <a href="#" className="text-orange-500 text-2xl mr-6"><FaYoutube /></a>
                    </div>
                </div>
                <div className="flex flex-col lg:flex-row">
                    <div className="flex flex-col mb-6 lg:mb-0 lg:mr-10">
                        <h3 className="text-xl font-semibold mb-2">SELSKAPET</h3>
                        <a href="#" className="mb-1 text-gray-600 hover:text-green-700 no-underline">Om</a>
                        <a href="#" className="mb-1 text-gray-600 hover:text-green-700 no-underline">Tjenester</a>
                        <a href="#" className="mb-1 text-gray-600 hover:text-green-700 no-underline">Samarbeid</a>
                        <a href="#" className="mb-1 text-gray-600 hover:text-green-700 no-underline">Produkter</a>
                        <a href="#" className="text-gray-600 hover:text-green-700 no-underline">MatBlog</a>
                    </div>
                    <div className="flex flex-col lg:ml-10">
                        <h3 className="text-xl font-semibold mb-6">NYESTE HENDELSER</h3>
                        <div className="flex items-center mb-1">
                        <img src={middag1} alt="Supermiddag på 10 minutter" className="w-18 h-14 mr-3 flex-shrink-0" />
                            <div>
                                <a href="#" className="font-semibold text-gray-800 hover:text-green-700">Supermiddag på 10 minutter</a>
                                <p className="text-sm text-gray-500">15 April 2024</p>
                            </div>
                        </div>
                        <div className="flex items-center"> 
                        <img src={middag3} alt="Supermiddag på 10 minutter" className="w-18 h-14 mr-3 flex-shrink-0" />
                            <div>
                                <a href="#" className="font-semibold text-gray-800 hover:text-green-700">10 tips for mer smak</a>
                                <p className="text-sm text-gray-500">15 April 2024</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="text-center text-gray-600 text-sm mt-6">
                © 2024 LokalmatAgder
                <span className="mx-1">|</span>
                <a href="#" className="hover:text-green-700 no-underline">Personvern</a>
                <span className="mx-1">|</span>
                <a href="#" className="hover:text-green-700 no-underline">Brukervilkår</a>
            </div>
        </footer>
    );
};

export default Footer;