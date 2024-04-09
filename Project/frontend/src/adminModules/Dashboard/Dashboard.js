import {Link, Route, Routes} from "react-router-dom";
import SomeAdminPage from "../Shared/Components/SomeAdminPage";
import SharedLayout from "../Shared/SharedLayout";
import Header from "../Shared/Components/Header/Header";
import Sidebar from "../Shared/Components/Sidebar/Sidebar";

import React, {useEffect, useState} from "react";
import StockComponent from "../Sales/Components/StockComponent";
import OrderComponent from "../Sales/Components/OrderComponent";
import TransporterComponent from "../Sales/Components/TransporterComponent";
import AdminControlComponent from "../Sales/Components/AdminControlComponent";
import {faUsers} from "@fortawesome/free-solid-svg-icons";
import SummaryCard from "./SummaryCard";
import {Card, Col, Row} from "react-bootstrap";
import BarChart from "./BarChart";
import RekoRingCard from "./RekoRingCard";
import CustomTable from "../Shared/SingleComponents/TableComponent";
import GeneralBottom from "../Shared/Components/GeneralBottom/GeneralBottom";



const testCardData = [
    {
        title: 'Omsetning',
        value: '30,000',
        sign: '+', // pass sign as "+" or "-" based on your data logic
        icon: faUsers,
    },
    {
        title: 'Retur fra salg',
        value: '30,000',
        sign: '+', // pass sign as "+" or "-" based on your data logic
        icon: faUsers,
    },

    {
        title: 'Kjøpt',
        value: '30,000',
        sign: '+', // pass sign as "+" or "-" based on your data logic
        icon: faUsers,
    },

    {
        title: 'Fortjeneste',
        value: '30,000',
        sign: '-', // pass sign as "+" or "-" based on your data logic
        icon: faUsers,
    },
];

const testChartData = {
    labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
    datasets: [
        {
            label: 'My First dataset',
            backgroundColor: 'green',
            borderColor: 'green',
            borderWidth: 1,
            hoverBackgroundColor: 'lightgreen',
            hoverBorderColor: 'rgba(255,99,132,1)',
            data: [65, 59, 80, 81, 56, 55, 40],
        },
        {
            label: 'My second dataset',
            backgroundColor: 'blue',
            borderColor: 'green',
            borderWidth: 1,
            hoverBackgroundColor: 'lightgreen',
            hoverBorderColor: 'rgba(255,99,132,1)',
            data: [35, 29, 10, 81, 156, 25, 60],
        },
    ],

};

const testRekoRingData = {
    title: 'Kommende REKO-ring',
    date: '12 Aug, 2024',
};

const testItemsForNotTreatedOrderTable = [
    { orderId: '#1245', customer: 'Ola', item: 'Potet', address: 'Hestaberg' },
    { orderId: '#1246', customer: 'Ola', item: 'Potet', address: 'Hestaberg' },
    { orderId: '#1247', customer: 'Ola', item: 'Potet', address: 'Hestaberg' }
    // Add more items as needed
];

const testItemsForTopProductsTable = [
    { product: 'Gulrot', amount: '12', price: '29.90,-'},
    { product: 'Eple', amount: '145', price: '9.90,-'},
    { product: 'Ost', amount: '12', price: '129.90,-'},

];


function Dashboard() {

    const [cardData, setCardData] = useState([]);
    const [chartData, setChartData] = useState({ labels: [], datasets: [] });
    const [rekoRingData, setRekoRingData] = useState({});
    const [itemsForNotTreatedOrderTable, setItemsForNotTreatedOrderTable] = useState([]);
    const [itemsForTopProductsTable, setItemsForTopProductsTable] = useState([]);

    const useTestData = true;

    useEffect(() => {
        async function fetchData() {
            if (useTestData) {
                // Use test data
                setCardData(testCardData);
                console.log(cardData);

                setChartData(testChartData);
                setRekoRingData(testRekoRingData);
                setItemsForNotTreatedOrderTable(testItemsForNotTreatedOrderTable);
                setItemsForTopProductsTable(testItemsForTopProductsTable);
            } else {
                try {
                    // Fetch data for the cards
                    const cardsResponse = await fetch('/api/cards');
                    const cardsData = await cardsResponse.json();
                    setCardData(cardsData);

                    // Fetch data for the chart
                    const chartResponse = await fetch('/api/chart');
                    const chartData = await chartResponse.json();
                    setChartData(chartData);

                    // Fetch data for the "Kommende REKO-ring" card
                    const rekoRingResponse = await fetch('/api/rekoRing');
                    const rekoRingData = await rekoRingResponse.json();
                    setRekoRingData(rekoRingData);

                    // Fetch data for the "Ikke behandlet" table
                    const firstTableResponse = await fetch('/api/firstTable');
                    const firstTableData = await firstTableResponse.json();
                    setItemsForNotTreatedOrderTable(firstTableData);

                    // Fetch data for the "Topp produkter" table
                    const secondTableResponse = await fetch('/api/secondTable');
                    const secondTableData = await secondTableResponse.json();
                    setItemsForTopProductsTable(secondTableData);
                } catch (error) {
                    console.error('Failed to fetch data', error);
                    // Handle errors as appropriate for your application
                }
            }
        }

        console.log("Using test data:", useTestData);

        fetchData();
    }, [useTestData]);

    const options = {
        maintainAspectRatio: false,
        scales: {
            y: { // Changed from 'yAxes' to 'y'
                beginAtZero: true,
                grid: {
                    display: false, // This will remove the gridlines along the x-axis
                    drawBorder: false, // This will remove the border line at the edge of the axis
                },
            },
            x: { // Changed from 'xAxes' to 'x'
                // Configuration for x-axis
                grid: {
                    display: false, // This will remove the gridlines along the y-axis
                    drawBorder: false, // This will remove the border line at the edge of the axis
                },
            },

        },
        plugins: {
            legend: {
                display: true, // Set this to false if you also want to remove the legend
            },
        },
    };

    // Define the columns for the tables
    const columnsForFirstTable = [
        { header: 'Ordre ID', accessor: 'orderId' },
        { header: 'Kunde', accessor: 'customer' },
        { header: 'Vare', accessor: 'item' },
        { header: 'Adresse', accessor: 'address' }
        // Add more columns as needed
    ];

    const columnsForSecondTable = [
        { header: 'Vare', accessor: 'product' },
        { header: 'Antall', accessor: 'amount' },
        { header: 'Pris', accessor: 'price' },
    ];

    return (
        <div>

            <Row xs={1} sm={2} md={2} lg={4} className="g-4">
                {cardData.map((data, idx) => (
                    <Col key={idx}>
                        <SummaryCard
                            title={data.title}
                            value={data.value}
                            sign={data.sign}
                            icon={data.icon}
                        />
                    </Col>
                ))}
            </Row>



            <Row xs={1} sm={2} md={4} className="g-4 mt-4">

                <Col xs={12} sm={8} md={8}>
                    <Card>

                            <Card.Header className="d-flex justify-content-between align-items-center bg-white m-3"
                                         style={{ borderBottom: '2px solid #343a40' }}>
                                <h5 className="mb-0">Ikke behandlet</h5>
                                <GeneralBottom
                                    variant="secondary"
                                    text="+ Skriv ut"
                                    className="mb-0"
                                    style={{padding: '8px 25px'}}
                                />
                            </Card.Header>
                            <Card.Body>
                                <CustomTable
                                    data={itemsForNotTreatedOrderTable}
                                    columns={columnsForFirstTable}
                                    className="mb-4" // Add more classes as needed
                                />
                            </Card.Body>






                    </Card>

                </Col>

                <Col xs={12} sm={4} md={4} className="d-flex justify-content-center">
                    <RekoRingCard {...rekoRingData} />
                </Col>
            </Row>


            <Row xs={1} sm={2} md={4} className="g-4 mt-4">

                <Col xs={12} sm={8} md={8}>
                    <Card>
                        <Card.Header className="d-flex justify-content-between align-items-center bg-white m-3">
                            <h5 className="mb-0">My Chart</h5>

                        </Card.Header>
                        <Card.Body>
                            <BarChart data={chartData} options={options} />
                        </Card.Body>
                    </Card>
                </Col>

                <Col xs={12} sm={4} md={4}>
                    <Card>
                        <Card.Header className="d-flex justify-content-between align-items-center bg-white m-3"
                                     style={{ borderBottom: '2px solid #343a40' }}>
                            <h5 className="mb-2">Topp produkter</h5>
                        </Card.Header>
                        <Card.Body>
                            <CustomTable
                                data={itemsForTopProductsTable}
                                columns={columnsForSecondTable}
                                className="mb-4" // Add more classes as needed
                            />

                        </Card.Body>
                    </Card>
                </Col>
            </Row>


            <p>-----------------------------------------------------------------------------</p>

            <h2>Hello. Oversikt over </h2>

            <p><Link to="/admin/some-admin-page">Daniel side</Link></p>

            <p><Link to="/admin/test-sharedlayout">Test SharedLayout</Link></p>
            <p><Link to="/admin/test-header">Test Header</Link></p>
            <p><Link to="/admin/test-sidebar">Test Sidebar</Link></p>

            <p>-----------------------------------------------------------------------------</p>

            <p><Link to="/admin/stock">Varelager</Link></p>
            <p><Link to="/admin/order">Ordre oversikt</Link></p>
            <p><Link to="/admin/transporter">Transportør oversikt</Link></p>
            <p><Link to="/admin/adminOverview">Anine oversikt</Link></p>

            <p>-----------------------------------------------------------------------------</p>
            <p><Link to="/admin/product-page">Go to Add Product Page</Link></p>
            <p><Link to="/admin/order-page">Go to Add Order Page</Link></p>
            <p><Link to="/admin/producer-page">Go to Produsentinfo side</Link></p>
            <p><Link to="/admin/transporter-page">Go to TransporterInfo side</Link></p>
            

            <Routes>
                <Route path="/some-admin-page" element={<SomeAdminPage />} />

                <Route path="/test-sharedlayout" element={<SharedLayout />} />
                <Route path="/test-header" element={<Header />} />
                <Route path="/test-sidebar" element={<Sidebar />} />

                <Route path="/some-admin-page" element={<SomeAdminPage/>}/>


                <Route path="/test-sharedlayout" element={<SharedLayout/>}/>
                <Route path="/test-header" element={<Header/>}/>
                <Route path="/test-sidebar" element={<Sidebar/>}/>
                <Route path="/stock" element={<StockComponent/>}/>
                <Route path="/ordre" element={<OrderComponent/>}/>
                <Route path="/transporter" element={<TransporterComponent/>}/>
                <Route path="/adminOverview" element={<AdminControlComponent/>}/>

                {/* Define more routes specific to the admin section */}
            </Routes>

        </div>
    );
}

export default Dashboard;

