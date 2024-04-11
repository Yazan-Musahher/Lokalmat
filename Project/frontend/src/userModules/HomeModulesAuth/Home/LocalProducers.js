import React, { useRef } from 'react';
import { Element } from 'react-scroll';
import NytNorge from '../../../Assets/Produsenter/NytNorge.png';
import Bama from '../../../Assets/Produsenter/Bama.png';
import Debio from '../../../Assets/Produsenter/Debio.png';
import Norvegia from '../../../Assets/Produsenter/Norvegia.png';


const LocalProducers = () => {
  const scrollContainerRef = useRef(null);

  const scroll = (scrollOffset) => {
    scrollContainerRef.current.scrollLeft += scrollOffset;
  };

  const producers = [
    { name: 'NytNorge', rating: 4.7, imageUrl: NytNorge },
    { name: 'Bama', rating: 5.0, imageUrl: Bama },
    { name: 'Debio', rating: 4.9, imageUrl: Debio },
    { name: 'DNorvegia', rating: 5.0, imageUrl: Norvegia},
  ];

  return (
    <Element id="producers" className="pt-16">
    <div className="bg-white py-6">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 mb-20">
        <div className="flex flex-col lg:flex-row justify-between items-center">
          <div className="mb-6 lg:mb-0">
            <div className="text-2xl font-bold text-green-800 mb-3">Agder</div>
            <div className="text-4xl font-semibold text-blue-gray-900">Våre lokale</div>
            <div className="text-4xl font-semibold text-blue-gray-900 mb-4">produsenter</div>
            <p className="font-bold text-lg">Den siste ukens topp produsenter</p>
          </div>
          <div className="relative w-full lg:w-auto">
            <button
              onClick={() => scroll(-200)}
              className="absolute left-0 z-10 bg-white rounded-full p-2 shadow-md hidden lg:block"
              aria-label="Scroll left"
            >
              {/* Replace with actual left arrow icon */}
              <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M15 19l-7-7 7-7" />
              </svg>
            </button>
            <div ref={scrollContainerRef} className="flex space-x-4 overflow-x-auto lg:overflow-hidden">
              {/* Producers Cards */}
              {producers.map((producer, index) => (
                <div key={index} className="min-w-max bg-gray-100 rounded-lg shadow p-4">
                  <img className="h-32 w-32 rounded-full mx-auto" src={producer.imageUrl} alt={producer.name} />
                  <div className="text-center mt-2">
                    <div className="text-lg text-gray-700">{producer.name}</div>
                    <div className="text-green-500">{producer.rating}</div>
                  </div>
                </div>
              ))}
            
            <button
              onClick={() => scroll(200)}
              className="absolute right-0 z-10 bg-white rounded-full p-2 shadow-md hidden lg:block"
              aria-label="Scroll right"
            >
              {/* Replace with actual right arrow icon */}
              <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M9 5l7 7-7 7" />
              </svg>
            </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    </Element>
  );
};

export default LocalProducers;
