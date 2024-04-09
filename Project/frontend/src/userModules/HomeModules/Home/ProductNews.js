import React from 'react';
import { Element } from 'react-scroll';

// Mock images, replace with your actual image paths
import bananaImage from '../../.././Assets/Productnews/Banana.jpg';
import vitality from '../../.././Assets/Productnews/vitality-shot.png';
import Vitality2 from '../../.././Assets/Productnews/Vitality-Shot2.jpg';
import jordbar from '../../.././Assets/Productnews/jordbar.jpg';
import varer from '../../.././Assets/Productnews/varer.jpeg';

const ProductNews = () => {
  // Define the categories for the navbar
  const categories = ['Alle', 'Ukens produsent', 'Vi anbefaler', 'Produsentens tips', 'Lokalt'];

  const newsItems = [
    { id: 1, title: 'sjekk ut dette 1', imageUrl: jordbar },
    { id: 2, title: 'sjekk ut dette 2', imageUrl: bananaImage },
    { id: 3, title: 'sjekk ut dette 3', imageUrl: vitality },
    { id: 4, title: 'sjekk ut dette 4', imageUrl: Vitality2 },
    { id: 5, title: 'sjekk ut dette 5', imageUrl: jordbar },
    { id: 6, title: 'sjekk ut dette 6', imageUrl: varer },
  ];

  return (
    <Element  id="news" className="pt-16">
    <div className="bg-white py-6 px-4">
      <div className="text-4xl font-bold text-center mb-6">Produkt nyheter</div>
      <div className="flex justify-center space-x-4 mb-6">
        {categories.map((category, index) => (
          <button
            key={index}
            className="text-sm font-semibold text-gray-600 hover:text-gray-900 px-3 py-1 rounded transition duration-300 ease-in-out focus:outline-none"
          >
            {category}
          </button>
        ))}
      </div>
      {/* Center the grid within a flex container */}
      <div className="flex justify-center">
        <div className="grid grid-cols-3 gap-2">
          {newsItems.map((item) => (
            <div key={item.id} className="rounded overflow-hidden shadow-lg">
              <div className="w-55 h-48">
                <img className="w-full h-full object-cover" src={item.imageUrl} alt={item.title} />
              </div>
              <div className="pt-2 text-center">
                <span className="text-gray-600 text-sm">{item.title}</span>
              </div>
            </div>
          ))}
        </div>
      </div>
      <div className="text-center mt-6">
        <button className="bg-green-700 text-white font-bold py-2 px-4 rounded">
          Se mer
        </button>
      </div>
    </div>
    </Element>
  );
};

export default ProductNews;
