import React, { useState } from 'react';
import {Tab, Tabs, Table, Button, Image, Row, Col, Form, ToggleButtonGroup, ToggleButton, Card} from 'react-bootstrap';
import ImageUpload from "./Storage/Components/ImageUpload/ImageUpload";
import Description from "./Storage/Components/Description/Description";
import CustomFormInput from "./Shared/Components/InputFields/CustomFormInput";
import GeneralBottom from "./Shared/Components/GeneralBottom/GeneralBottom";
import "./ProducerProfilePage.css"
import TableComponent from "./Shared/SingleComponents/TableComponent";
import CustomTable from "./Shared/SingleComponents/TableComponent";

const ProducerProfilePage = () => {
    const [activeTab, setActiveTab] = useState('generalInfo');

    // Assuming you have some producer information to display
    const [producerDetails, setProducerDetails] = useState({
        producerOrgNumber: '984 661 185',
        producerEmail: "produsent@gmail.com",
        image: null,
        producerName: 'Tine',
        producerPhone: '987 65 432',
        producerAddress: 'Storhaugen 43, 7790',
        producerDistrict: 'Agder',
        producerDescription: 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry\'s standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specim',
        errors: {},
        userErrors: ''
    });


    const handleImageSelected = (croppedImageUrl) => {
        setProducerDetails((prevDetails) => ({
            ...prevDetails,
            image: croppedImageUrl, // Now this will be the URL of the cropped image
        }));
    };


    const handleInputChange = (e) => {
        const { name, value } = e.target;

        // Validate the input
        const error = validateInput(name, value);

        // Set input value and error message
        setProducerDetails(prevDetails => ({
            ...prevDetails,
            [name]: value,
            errors: {
                ...prevDetails.errors,
                [name]: error,
            }
        }));
    };

    const handleDescriptionChange = (newDescription) => {
        setProducerDetails(prevDetails => ({
            ...prevDetails,
            description: newDescription
        }));
    };


    const handleSubmit = async () => {
        // Validate required fields before submission
        const errors = {};
        // Define the fields required for the transporter
        const requiredFields = [
            'producerEmail', 'producerName', 'producerPhone', 'producerAddress',
            'producerDistrict'
        ];

        requiredFields.forEach(field => {
            const error = validateInput(field, producerDetails[field]);
            if (error) errors[field] = error;
        });

        // Update the state with any validation errors
        setProducerDetails(prevDetails => ({
            ...prevDetails,
            errors: errors
        }));

        if (Object.keys(errors).length === 0) {
            const method = 'PUT';
            const endpoint = `${process.env.REACT_APP_API_URL}/transporters/${3}}`;

            // Construct the data object you're going to send
            const payload = {
                producerOrgNumber: '984 661 185',
                image: producerDetails.image,
                producerDescription: '',
                producerName: producerDetails.producerName,
                producerEmail: producerDetails.producerEmail,
                producerPhone: producerDetails.producerPhone,
                producerAddress: producerDetails.producerAddress,
                producerDistrict: producerDetails.producerDistrict,
            };


            console.log('Sending the following data to the API:', payload);

            try {
                const response = await fetch(endpoint, {
                    method: method,
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(payload),
                });


                if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);

                const data = await response.json();
                console.log('Transporter processed successfully:', data);
                // Handle success (e.g., clear form, show message, redirect)

            } catch (error) {
                console.error('There was a problem with the fetch operation:', error);
                // Handle error (e.g., show error message)
            }
        } else {
            console.log('Form contains errors:', errors);
            // Handle form validation errors
            setProducerDetails(prevDetails => ({
                ...prevDetails,
                userErrors: 'Det skjedde en feil. Vennligst prøv igjen senere.'
            }));

        }
    };


    const validateInput = (name, value) => {
        if (!value || !value.trim()) {
            return `The ${name} field is required.`;
        }
        return '';
    };
    const handleDeleteProfile = async () => {
        // Confirm deletion with the user before proceeding
        if (window.confirm('Er du sikker på at du vil slette denne profilen?')) {
            try {
                const response = await fetch(`api/products/productDetails.id`, {
                    method: 'DELETE',
                    // Additional headers and/or authorization if needed
                });

                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }

                console.log('Product deleted successfully');
                // Redirect to products list or update UI accordingly
            } catch (error) {
                console.error('There was a problem with the fetch operation:', error);
            }
        }
    };


    const inventoryItems = [
        { name: 'Epler', salesPerMonth: '86.71 kg', revenue: '100 000 kr', incomePerMonth: '20 000 kr', stock: '60/90' },
        { name: 'Paprika', salesPerMonth: '3 stk', revenue: '100 000 kr', incomePerMonth: '20 000 kr', stock: '60/90' },
        // ... more items
    ];

    const columns = [
        { header: 'Varenavn', accessor: 'name' },
        { header: 'Salg pr/ mnd', accessor: 'salesPerMonth' },
        { header: 'Omsetning', accessor: 'revenue' },
        { header: 'Inntekt pr/ mnd', accessor: 'incomePerMonth' },
        {
            header: 'Ny beholdning',
            accessor: 'stock',
            render: item => (
                // Custom rendering, for example a progress bar
                <div style={{ width: '100%', backgroundColor: '#ddd', borderRadius: '10px' }}>
                    <div
                        style={{
                            width: item.stock, // This should be a calculated width based on the stock value
                            backgroundColor: 'green', // You can have logic to change the color based on the value
                            color: 'white',
                            borderRadius: '10px'
                        }}
                    >
                        {item.stock}
                    </div>
                </div>
            )
        },
    ];


    const subscriptionData = [
        {
            period: 'September 2023',
            revenue: '2999,- kr',
            commission: 'Ingen',
            subscriptionActive: 'Nei',
            invoice: '-',
            status: '-'
        },
        {
            period: 'Oktober 2023',
            revenue: '13000,- kr',
            commission: '5%',
            subscriptionActive: 'Ja',
            invoice: '550,- kr',
            status: 'Betalt'
        },
        // ... add more data objects for each period as needed
    ];

    const subscriptionColumns = [
        { header: 'Periode', accessor: 'period' },
        { header: 'Omsetning', accessor: 'revenue' },
        { header: 'Provisjon', accessor: 'commission' },
        { header: 'Abonnement aktivt:', accessor: 'subscriptionActive' },
        { header: 'Faktura:', accessor: 'invoice' },
        {
            header: 'Status:',
            accessor: 'status',
            render: item => {
                // Define the width, color, and text based on the status value
                const barStyle = item.status === 'Betalt' ? {
                    width: '100%', // Assuming you want the bar to be full width for 'Betalt'
                    backgroundColor: 'green',
                    color: 'white',
                    borderRadius: '10px',
                    textAlign: 'center'
                } : {
                    width: '100%', // Same here, adjust based on your logic
                    backgroundColor: '#ddd',
                    color: 'black',
                    borderRadius: '10px',
                    textAlign: 'center'
                };

                return (
                    <div style={{ width: '100%', backgroundColor: '#ddd', borderRadius: '10px' }}>
                        <div style={barStyle}>
                            {item.status}
                        </div>
                    </div>
                );
            }
        },
        // ... other column definitions
    ];



    return (
        <div className="container mt-4">
            <Card>
                <Card.Header as="h3" className="d-flex justify-content-between align-items-center">
                    <span>Produsent info: {producerDetails.producerName}</span>
                    <Button variant="outline-danger" onClick={handleDeleteProfile}>
                        Slett profil
                    </Button>

                </Card.Header>
                <Card.Body>
                    <Tabs activeKey={activeTab} onSelect={(k) => setActiveTab(k)} id="product-info-tabs" className="mb-3">
                        <Tab eventKey="generalInfo" title={<span className={`tab-custom ${activeTab === 'generalInfo' ? 'active' : 'inactive'}`}>Generell info</span>}>
                            {/* Content for General Info */}
                        </Tab>
                        <Tab eventKey="storage" title={<span className={`tab-custom ${activeTab === 'storage' ? 'active' : 'inactive'}`}>Varelager</span>}>
                            {/* Content for Customer Perspective */}
                        </Tab>
                        <Tab eventKey="subscription" title={<span className={`tab-custom ${activeTab === 'subscription' ? 'active' : 'inactive'}`}>Abonnement</span>}>
                            {/* Content for Feedback */}
                        </Tab>
                    </Tabs>


                    <Row>
                        <Col md={12} className="d-flex justify-content-end text-muted">
                            <span>Org.nr.: {producerDetails.producerOrgNumber}</span>
                        </Col>
                    </Row>

                    {producerDetails.userErrors && (
                        <div className="alert alert-danger" role="alert">
                            {producerDetails.userErrors}
                        </div>
                    )}

                    <Row>
                        <Col sm={8} md={8} lg={4}>
                            <ImageUpload onImageSelected={handleImageSelected}/>
                        </Col>
                        <Col sm={12} md={12} lg={4}>
                            <Form>
                                <CustomFormInput
                                    controlId="producerName"
                                    label="Produsent*"
                                    type="text"
                                    placeholder="Skriv inn produsent"
                                    value={producerDetails.producerName}
                                    onChange={handleInputChange}
                                    name="producerName"
                                    error={producerDetails.errors.producerName}
                                    labelSize={{xs: 5, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 6 }}
                                />

                                <CustomFormInput
                                    controlId="producerEmail"
                                    label="Email*"
                                    type="text"
                                    placeholder="Skriv inn email"
                                    value={producerDetails.producerEmail}
                                    onChange={handleInputChange}
                                    name="producerEmail"
                                    error={producerDetails.errors.producerEmail}
                                    labelSize={{xs: 7, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 12, sm: 8, md: 6, lg: 6 }}
                                />

                                <CustomFormInput
                                    controlId="producerPhone"
                                    label="Telefon*"
                                    type="text"
                                    placeholder="Skriv inn tlf."
                                    value={producerDetails.producerPhone}
                                    onChange={handleInputChange}
                                    name="producerPhone"
                                    error={producerDetails.errors.producerPhone}
                                    labelSize={{xs: 5, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 6 }}
                                />

                                <CustomFormInput
                                    controlId="producerAddress"
                                    label="Adresse*"
                                    type="text"
                                    placeholder="Skriv inn adresse"
                                    value={producerDetails.producerAddress}
                                    onChange={handleInputChange}
                                    name="producerAddress"
                                    error={producerDetails.errors.producerAddress}
                                    labelSize={{xs: 5, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 12, sm: 8, md: 6, lg: 6 }}
                                />


                                <Description onDescriptionChange={handleDescriptionChange} showSuggestions={false} />
                            </Form>
                        </Col>

                        <Col sm={12} md={12} lg={4}>

                            <TableComponent data={inventoryItems} columns={columns} />
                        </Col>
                    </Row>


                    <CustomTable
                        data={subscriptionData}
                        columns={subscriptionColumns}
                    />


                    {producerDetails.userErrors && (
                        <div className="alert alert-danger" role="alert">
                            {producerDetails.userErrors}
                        </div>
                    )}

                    <Row className="justify-content-end mt-5">
                        <Col xs={7} md={4} lg={3}>
                            <GeneralBottom
                                variant="secondary"
                                text="Lagre endringer"
                                className="btn-custom-submit-producer"
                                onClick={handleSubmit}
                            />
                        </Col>

                        <Col xs={4} md={3} lg={2} className="me-4 mb-3">
                            <GeneralBottom
                                variant="neutral"
                                text="Tilbake"
                                className="btn-custom-cancel-producer"
                                onClick={() => console.log('Clicked "Tilbake" button')}
                            />
                        </Col>
                    </Row>
                </Card.Body>
            </Card>
        </div>
    );
}

export default ProducerProfilePage;
