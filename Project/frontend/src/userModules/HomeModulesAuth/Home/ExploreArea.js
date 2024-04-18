import React, { useState, useEffect } from 'react';
import { Element } from 'react-scroll';
import NytNorge from '../../../Assets/Produsenter/NytNorge.png';
import Bama from '../../../Assets/Produsenter/Bama.png';
import Debio from '../../../Assets/Produsenter/Debio.png';
import Norvegia from '../../../Assets/Produsenter/Norvegia.png';

const ExploreArea = () => {
  const [cities, setCities] = useState([]);
  const [selectedCities, setSelectedCities] = useState(new Set());
  const [products, setProducts] = useState([]);
  const [priceFrom, setPriceFrom] = useState('');
  const [priceTo, setPriceTo] = useState('');

  useEffect(() => {
    // Fetch cities
    fetch('http://localhost:5176/api/Product/cities')
      .then(response => response.json())
      .then(data => {
        setCities(data.map(city => ({ name: city, count: Math.floor(Math.random() * 1000) }))); // Dummy count value
      })
      .catch(error => {
        console.error('Error fetching cities:', error);
      });
    // Fetch all products by default
    fetchProducts();
  }, []);

  const fetchProducts = (citiesQuery = '', minPrice = '', maxPrice = '') => {
    let url = `http://localhost:5176/api/Product?`;
    const params = new URLSearchParams();

    if (citiesQuery) {
      params.append('city', citiesQuery);
    }
    if (minPrice) {
      params.append('minPrice', minPrice);
    }
    if (maxPrice) {
      params.append('maxPrice', maxPrice);
    }

    url += params.toString();

    fetch(url)
      .then(response => response.json())
      .then(data => {
        setProducts(data);
      })
      .catch(error => {
        console.error('Error fetching products:', error);
      });
  };

  const handleCheckboxChange = (cityName) => {
    setSelectedCities((prevSelectedCities) => {
      const updatedSelectedCities = new Set(prevSelectedCities);
      if (updatedSelectedCities.has(cityName)) {
        updatedSelectedCities.delete(cityName);
      } else {
        updatedSelectedCities.add(cityName);
      }
      const citiesQuery = Array.from(updatedSelectedCities).join(',');
      fetchProducts(citiesQuery, priceFrom, priceTo);
      return updatedSelectedCities;
    });
  };

  const handleSearch = () => {
    const citiesQuery = Array.from(selectedCities).join(',');
    fetchProducts(citiesQuery, priceFrom, priceTo);
  };

  return (
    <Element id="products" className="pt-19">
      <div className="container mx-auto flex flex-col lg:flex-row">
        <div className="lg:w-1/4 bg-white p-5 mb-5 lg:mb-0">
          <h2 className="text-xl font-semibold mb-4 mt-4 ml-8">Område</h2>
          <ul className="text-gray-700">
            {cities.map((city, index) => (
              <li key={index} className="flex justify-between items-center p-2 hover:bg-gray-100 cursor-pointer">
                <label className="flex items-center space-x-3">
                  <input 
                    type="checkbox" 
                    checked={selectedCities.has(city.name)}
                    onChange={() => handleCheckboxChange(city.name)}
                    className="form-checkbox h-5 w-5 text-blue-600"
                  />
                  <span className="text-sm font-medium text-gray-700">{city.name}</span>
                </label>
                <span className="text-sm font-medium text-gray-500">{`(${city.count})`}</span>
              </li>
            ))}
          </ul>
          {/* Price filter */}
          <div className="my-4 ml-8">
            <h2 className="text-xl font-semibold mb-3">Pris</h2>
            <div className="flex items-center gap-2">
              <input 
                type="text"
                placeholder=" Fra kr"
                value={priceFrom}
                onChange={(e) => setPriceFrom(e.target.value)}
                className="border-2 border-gray-300 focus:border-blue-500 focus:ring-blue-500 text-gray-700 text-sm w-20 h-10"
              />
              <input 
                type="text"
                placeholder=" Til kr"
                value={priceTo}
                onChange={(e) => setPriceTo(e.target.value)}
                className="border-2 border-gray-300 focus:border-blue-500 focus:ring-blue-500 text-gray-700 text-sm w-20 h-10"
              />
              <button 
                onClick={handleSearch}
                className="bg-green-700 text-white text-sm h-8 px-3 transition duration-150 ease-in-out transform hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50"
              >
                Søk
              </button>
            </div>
          </div>
        </div>
        {/* Products display */}
        <div className="flex-grow p-5">
          <div className="grid grid-cols-3 gap-10">
            {products.map((product) => (
              <div key={product.id} className="border p-4">
                <img src={product.imageUrl} alt={product.name} className="w-full h-48 object-cover" />
                <h3 className="text-lg font-semibold my-2">{product.name}</h3>
                <p className="text-gray-600">{`${product.price} kr`}</p>
                <p className="text-sm text-gray-500">{product.description}</p>
                {/* ... additional product details ... */}
              </div>
            ))}
          </div>
        </div>
      </div>
      {/* VÅRE SAMARBEIDSPARTNERE section now moved down */}
      <div className="mt-8">
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
      </div>
    </Element>
  );
};

export default ExploreArea;
