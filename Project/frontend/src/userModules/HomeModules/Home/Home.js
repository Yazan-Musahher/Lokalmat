import React from 'react';
import NavbarHome from '../Navbar/NavbarHome';
import Header from './Header';
import ExploreArea from './ExploreArea';
import LocalProducers from './LocalProducers';
import ProductNews from './ProductNews';
import Footer from './Footer';

const Home = () => {
    return (
        <>
            <NavbarHome />
            <Header />
            <ExploreArea />
            <ProductNews/>
            <Footer/>
        </>
    );
};

export default Home;
