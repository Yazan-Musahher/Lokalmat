import React, { useState, useEffect  } from 'react';
import { Element } from 'react-scroll';
import NytNorge from '../../../Assets/Produsenter/NytNorge.png';
import Bama from '../../../Assets/Produsenter/Bama.png';
import Debio from '../../../Assets/Produsenter/Debio.png';
import Norvegia from '../../../Assets/Produsenter/Norvegia.png';
import map02 from '../../../Assets/Produsenter/map02.png';




// Filter for sted,pris etc.
const Filters = ({ onCityButtonClick }) => {
  return (
    <div className="my-6">
      <div className="flex flex-col space-y-2">
        <button
          onClick={onCityButtonClick}
          className="text-sm border border-gray-300 rounded-full py-2.5 bg-white shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-gray-200 focus:ring-opacity-50 w-50 flex justify-between items-center"
        >
          <span className="flex-grow text-left pl-4">Sted</span>
          <span className="pr-4">
            <svg className="h-5 w-5 text-slate-600"  width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z"/>
              <polyline points="6 9 12 15 18 9" />
            </svg>
          </span>
        </button>
        <button className="text-sm border border-gray-300 rounded-full py-2.5 bg-white shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-gray-200 focus:ring-opacity-50 w-50 flex justify-between items-center">
          <span className="flex-grow text-left pl-4">Pris</span>
          <span className="pr-4">
            <svg className="h-5 w-5 text-slate-600"  width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z"/>
              <polyline points="6 9 12 15 18 9" />
            </svg>
          </span>
        </button>
        <button className="text-sm border border-gray-300 rounded-full py-2.5 bg-white shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-gray-200 focus:ring-opacity-50 w-50 flex justify-between items-center">
          <span className="flex-grow text-left pl-4">Popularitet</span>
          <span className="pr-4">
            <svg className="h-5 w-5 text-slate-600"  width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z"/>
              <polyline points="6 9 12 15 18 9" />
            </svg>
          </span>
        </button>
        <button className="text-sm border border-gray-300 rounded-full py-2.5 bg-white shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-gray-200 focus:ring-opacity-50 w-50 flex justify-between items-center">
          <span className="flex-grow text-left pl-4">Rangering</span>
          <span className="pr-4">
            <svg className="h-5 w-5 text-slate-600"  width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">
              <path stroke="none" d="M0 0h24v24H0z"/>
              <polyline points="6 9 12 15 18 9" />
            </svg>
          </span>
        </button>
      </div>
    </div>
  );
};



const ExploreArea = () => {
  const [inputValue, setInputValue] = useState('');
  const [cities, setCities] = useState([]);

  const fetchCities = () => {
    console.log("Fetching cities from the backend..."); // Confirm the fetch starts
    fetch('http://localhost:5176/api/Product/cities')
      .then((response) => {
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.json();
      })
      .then((data) => {
        console.log("Cities fetched successfully:", data); // Check the fetched data
        setCities(data);
      })
      .catch((error) => {
        console.error('Error fetching cities:', error);
      });
  };

  const handleInputChange = (e) => {
    setInputValue(e.target.value);
  };

  const handleCancel = () => {
    setInputValue('');
  };


  return (
    <>
      <Element id="products" className="pt-19">
        <div className="flex items-center justify-center min-h-64">
          <h1 className="text-5xl font-bold text-gray-800 leading-none">
            VÅRE SAMARBEIDSPARTNERE
          </h1>
        </div>
        <div className="flex items-center justify-center overflow-x-auto py-8">
          <img src={NytNorge} alt="Nyt Norge" className="mx-4" style={{ width: '100px', height: '100px' }} />
          <img src={Bama} alt="Bama" className="mx-4" style={{ width: '100px', height: '100px' }} />
          <img src={Debio} alt="Debio" className="mx-4" style={{ width: '100px', height: '100px' }} />
          <img src={Norvegia} alt="Norvegia" className="mx-4" style={{ width: '100px', height: '100px' }} />
        </div>
        <div className="flex justify-center items-start flex-wrap md:flex-nowrap mb-10">
          <div className="max-w-lg p-8">
            <p className="text-3xl font-bold text-gray-700 mb-8">
              Utforsk ditt nærområde
            </p>
            <p className="text-lg text-gray-600 mb-6">
              Legg til filter for mer presise søk, og utforsk hva ditt lokale næringsliv har å tilby.
            </p>
            <Filters onCityButtonClick={fetchCities} />
            {/* Search bar */}
            <div className="mt-4 flex items-center space-x-2">
              <div className="bg-white border border-gray-300 rounded-full flex items-center px-4 py-2">
                <input
                  className="bg-transparent flex-grow outline-none"
                  type="text"
                  placeholder="Søk etter produkter"
                  value={inputValue}
                  onChange={handleInputChange}
                />
                {inputValue && (
                  <button onClick={handleCancel} className="text-red-500 focus:outline-none">
                    ×
                  </button>
                )}
              </div>
              {inputValue && (
                <div onClick={handleCancel} className="text-red-500 ml-4 cursor-pointer">
                  Avbryt
                </div>
              )}
            </div>
            {/* Product Stats and Search Button */}
            <div className="flex justify-between items-center mt-4">
              <div className="text-center">
                <div className="text-3xl font-bold">1900</div>
                <div className="text-sm text-gray-600">Totale produkter</div>
              </div>
              <div className="text-center">
                <div className="text-3xl font-bold">500</div>
                <div className="text-sm text-gray-600">Tilgjengelige i ditt nærområde</div>
              </div>
              <button className="bg-green-700 text-white rounded-full px-8 py-2">
                Søk
              </button>
            </div>
          </div>
          <div className="ml-20">
            <img src={map02} alt="map02" style={{ width: '500px', height: '500px' }} />
          </div>
        </div>
      </Element>
      {/* Dropdown to display cities */}
      {cities.length > 0 && (
        <div style={{
          position: 'absolute', top: '100px', left: '50%', transform: 'translateX(-50%)',
          backgroundColor: 'white', border: '1px solid black', padding: '10px', borderRadius: '5px',
          boxShadow: '0 2px 5px rgba(0,0,0,0.2)', zIndex: 1000
        }}>
          {cities.map((city, index) => (
            <div key={index} className="py-2 hover:bg-gray-100 cursor-pointer">
              {city}
            </div>
          ))}
        </div>
      )}
    </>
  );
};

export default ExploreArea;
