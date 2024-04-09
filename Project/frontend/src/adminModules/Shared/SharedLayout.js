import React, {useEffect, useState} from 'react';
import { Outlet } from 'react-router-dom';
import './SharedLayout.css'; // You'll create this CSS file next

import Header from './Components/Header/Header';
import Sidebar from './Components/Sidebar/Sidebar';


function SharedLayout() {

    const [isSidebarOpen, setIsSidebarOpen] = useState(window.innerWidth > 768);

    const toggleSidebar = () => {
        setIsSidebarOpen(!isSidebarOpen);
    };

    // Handle resizing
    useEffect(() => {
        function handleResize() {
            // Automatically open the sidebar on larger screens and close on smaller screens
            setIsSidebarOpen(window.innerWidth > 768);
        }

        // Add event listener
        window.addEventListener('resize', handleResize);

        // Call the handler right away so state gets updated with initial window size
        handleResize();

        // Remove event listener on cleanup
        return () => window.removeEventListener('resize', handleResize);
    }, []); // Emp


    return (
        <>
            <Header toggleSidebar={toggleSidebar} />
            <Sidebar isSidebarOpen={isSidebarOpen} toggleSidebar={toggleSidebar} />
            <main className="content-area">
                <div className={isSidebarOpen ? 'main-content-expanded' : 'main-content'}>
                <Outlet /> {
                    <div className={isSidebarOpen ? 'main-content-expanded' : 'main-content'}>

                    </div>
                }
                </div>
            </main>
        </>
    );
}

export default SharedLayout;