import React from 'react';
import NavbarHome from '../Navbar/NavbarHomeAuth';
import Header from './Header';
import ExploreArea from './ExploreArea';
import LocalProducers from './LocalProducers';
import ProductNews from './ProductNews';
import Footer from './Footer';

const HomeAuth = () => {
    return (
        <>
            <NavbarHome />
            <Header />
            <ExploreArea />
            <LocalProducers/>
            <ProductNews/>
            <Footer/>
        </>
    );
};

export default HomeAuth;
