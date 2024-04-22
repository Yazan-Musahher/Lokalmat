import {Card, Col, Form, Row, Tab, Tabs, ToggleButton, ToggleButtonGroup} from "react-bootstrap";
import Button from "react-bootstrap/Button";
import ImageUpload from "../Storage/Components/ImageUpload/ImageUpload";
import CustomFormInput from "../Shared/Components/InputFields/CustomFormInput";
import PriceAndDiscountSection from "../Storage/Components/PriceAndDiscountSection/PriceAndDiscountSection";
import DeliveryMethodSelect from "../Storage/Components/DeliveryMethodSelect/DeliveryMethodSelect";
import Description from "../Storage/Components/Description/Description";
import GeneralBottom from "../Shared/Components/GeneralBottom/GeneralBottom";
import React, {useEffect, useState} from "react";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faPrint} from "@fortawesome/free-solid-svg-icons";
import "./OrderPage.css"
import {useNavigate, useParams} from "react-router-dom";


const mockOrderDetails = {
    '1': {
        date: '2024-04-02', // Format YYYY-MM-DD
        orderNumber: 'ORD-001',
        image: null,
        customerName: 'Dan',
        customerEmail: 'jane@example.com',
        orderProduct: 'Product 1',
        orderQuantity: '24',
        price: '19.99',
        discountApplied: false,
        deliveryMethod: 'bring',
        customerAddress: 'Hestaberg',
        customerHouseNumber: '450',
        customerPostNumber: '5570',
        customerLocation: 'Haugesund',
        status: 'ikke ferdig',
        paid: false,
        description: 'Dette er ID test.',
        errors: {},
        userErrors: ''
    },
    '2': {
        date: '2024-04-02', // Format YYYY-MM-DD
        orderNumber: 'ORD-001',
        image: null,
        customerName: 'Hest',
        customerEmail: 'jaaada@example.com',
        orderProduct: 'Product 2',
        orderQuantity: '54',
        price: '29.99',
        discountApplied: false,
        deliveryMethod: 'posten',
        customerAddress: 'Hestaberg',
        customerHouseNumber: '5',
        customerPostNumber: '5571',
        customerLocation: 'Haugesund',
        status: 'ikke ferdig',
        paid: false,
        description: 'Dette er ID test 2.',
        errors: {},
        userErrors: ''
    },
    // Add more mock orders as needed...
};

function OrderPage() {

    const navigate = useNavigate();

    const { orderId } = useParams();

    const [activeTab, setActiveTab] = useState('generalInfo');

    const [isEditMode, setIsEditMode] = useState(false); //If the product is editing instead of adding

    const [orderDetails, setOrderDetails] = useState({
        date: formatDate(new Date()), // Format YYYY-MM-DD
        orderNumber: '',
        image: null,
        customerName: '',
        customerEmail: '',
        orderProduct: '',
        orderQuantity: '',
        price: '',
        discountApplied: false,
        deliveryMethod: '',
        customerAddress: '',
        customerHouseNumber: '',
        customerPostNumber: '',
        customerLocation: '',
        status: 'ikke ferdig',
        paid: false,
        description: '',
        errors: {},
        userErrors: ''
    });


    useEffect(() => {
        if (orderId) {
            setIsEditMode(true);

            //fetchOrderDetails(orderId);

            const details = mockOrderDetails[orderId];
            console.log('Found details:', details);
            if (details) {
                setOrderDetails(details);
            } else {
                console.error("Order details not found for orderId:", orderId);
                // Handle order not found
            }

        } else {
            setIsEditMode(false);
            // Set orderDetails to default values for a new order
        }
    }, [orderId]);

    const fetchOrderDetails = async (orderId) => {
        try {
            const response = await fetch(`/api/orders/${orderId}`);
            if (!response.ok) {
                throw new Error('Failed to fetch order details');
            }
            const details = await response.json();
            setOrderDetails(details);
        } catch (error) {
            console.error("Fetching order details failed:", error);
            // Handle error state appropriately
        }
    };


    function formatDate(date) {
        const d = new Date(date),
            day = '' + d.getDate(),
            month = '' + (d.getMonth() + 1),
            year = d.getFullYear();

        return [day.padStart(2, '0'), month.padStart(2, '0'), year].join('.');
    }

    const handleImageSelected = (imageFile) => {
        setOrderDetails((prevDetails) => ({
            ...prevDetails,
            image: imageFile,
        }));
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;

        // Validate the input
        const error = validateInput(name, value);

        // Set input value and error message
        setOrderDetails(prevDetails => ({
            ...prevDetails,
            [name]: value,
            errors: {
                ...prevDetails.errors,
                [name]: error,
            }
        }));
    };


    const handleDeliveryMethodChange = (e) => {
        const newDeliveryMethod = e.target.value;
        setOrderDetails(prevDetails => ({
            ...prevDetails,
            deliveryMethod: newDeliveryMethod,
        }));
    };

    const handleStatusChange = (newStatus) => {
        setOrderDetails(prevDetails => ({
            ...prevDetails,
            status: newStatus, // Directly set newStatus
        }));
    };


    const handlePaymentChange = (isPaid) => {
        setOrderDetails(prevDetails => ({
            ...prevDetails,
            paid: isPaid,
        }));
    };


    const handleDescriptionChange = (newDescription) => {
        setOrderDetails(prevDetails => ({
            ...prevDetails,
            description: newDescription
        }));
    };


    const handleSubmit = async () => {
        // Validate required fields before submission
        const errors = {};
        // Define the fields required for the order
        const requiredFields = [
            'customerName', 'customerEmail', 'orderProduct', 'orderQuantity',
            'price', 'deliveryMethod', 'customerAddress', 'customerHouseNumber',
            'customerPostNumber', 'customerLocation'
        ];

        requiredFields.forEach(field => {
            const error = validateInput(field, orderDetails[field]);
            if (error) errors[field] = error;
        });

        // Update the state with any validation errors
        setOrderDetails(prevDetails => ({
            ...prevDetails,
            errors: errors
        }));

        if (Object.keys(errors).length === 0) {
            const method = isEditMode ? 'PUT' : 'POST';
            const endpoint = `${process.env.REACT_APP_API_URL}/orders${isEditMode ? `/${orderId}` : ''}`;

            // Construct the data object you're going to send
            const payload = {
                date: orderDetails.date,
                orderNumber: orderDetails.orderNumber,
                image: orderDetails.image,
                customerName: orderDetails.customerName,
                customerEmail: orderDetails.customerEmail,
                orderProduct: orderDetails.orderProduct,
                orderQuantity: orderDetails.orderQuantity,
                price: orderDetails.price,
                deliveryMethod: orderDetails.deliveryMethod,
                customerAddress: orderDetails.customerAddress,
                customerHouseNumber: orderDetails.customerHouseNumber,
                customerPostNumber: orderDetails.customerPostNumber,
                customerLocation: orderDetails.customerLocation,
                status: orderDetails.status,
                paid: orderDetails.paid,
                // Include any other fields required by your API
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
                console.log('Order processed successfully:', data);
                // Handle success (e.g., clear form, show message, redirect)

            } catch (error) {
                console.error('There was a problem with the fetch operation:', error);
                // Handle error (e.g., show error message)
            }
        } else {
            console.log('Form contains errors:', errors);
            // Handle form validation errors
            setOrderDetails(prevDetails => ({
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

    const handleDeleteOrder = async () => {
        // Confirm deletion with the user before proceeding
        if (window.confirm('Er du sikker på at du vil slette denne ordren?')) {
            try {
                const response = await fetch(`api/products/${orderDetails.id}`, {
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

    return (
        <div className="container mt-4">
            <Card>
                <Card.Header as="h3" className="d-flex justify-content-between align-items-center">
                    <span>{isEditMode ? `Endre ordre: ${orderDetails.orderNumber}` : "Legg til ordre"}</span>
                    {isEditMode && (
                        <Button variant="outline-danger" onClick={handleDeleteOrder}>
                            Slett ordre
                        </Button>
                    )}
                </Card.Header>
                <Card.Body>
                    <Tabs activeKey={activeTab} onSelect={(k) => setActiveTab(k)} id="product-info-tabs" className="mb-3">
                        <Tab eventKey="generalInfo" title={<span className={`tab-custom ${activeTab === 'generalInfo' ? 'active' : 'inactive'}`}>Generell info</span>}>
                            {/* Content for General Info */}
                        </Tab>
                        <Tab eventKey="customerPerspective" title={<span className={`tab-custom ${activeTab === 'customerPerspective' ? 'active' : 'inactive'}`}>Anbud</span>}>
                            {/* Content for Customer Perspective */}
                        </Tab>
                        <Tab eventKey="feedback" title={<span className={`tab-custom ${activeTab === 'feedback' ? 'active' : 'inactive'}`}>Tilbakemeldinger</span>}>
                            {/* Content for Feedback */}
                        </Tab>
                    </Tabs>



                    {/* Rest of your product page content */}

                    <Row>
                        <Col md={12} className="d-flex justify-content-end text-muted">
                            <span>Ordrenummer: {orderDetails.orderNumber || "Tilordnes"}</span>
                        </Col>
                    </Row>
                    <Row>
                        <Col md={12} className="d-flex justify-content-end mb-4 text-muted">
                            <span>Dato: {orderDetails.date}</span>
                        </Col>
                    </Row>


                    <Row>
                        {/* First column for image */}
                        <Col xs={12} md={12} lg={6} >

                            <ImageUpload onImageSelected={handleImageSelected} />

                            <Description onDescriptionChange={handleDescriptionChange} value={orderDetails.description} showSuggestions={false} />
                        </Col>



                        {/* Second column for product details form */}
                        <Col xs={12} md={12} lg={6}>

                            <Form>
                                <CustomFormInput
                                    controlId="customerName"
                                    label="Kunde navn*"
                                    type="text"
                                    placeholder="Skriv inn kunde navn"
                                    value={orderDetails.customerName}
                                    onChange={handleInputChange}
                                    name="customerName"
                                    error={orderDetails.errors.customerName}
                                    labelSize={{xs: 4, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 5 }}
                                />


                                <CustomFormInput
                                    controlId="customerEmail"
                                    label="Email*"
                                    type="text"
                                    placeholder="Skriv inn kunde email"
                                    value={orderDetails.customerEmail}
                                    onChange={handleInputChange}
                                    name="customerEmail"
                                    error={orderDetails.errors.customerEmail}
                                    labelSize={{xs: 4, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 5 }}
                                />

                                <CustomFormInput
                                    controlId="orderProduct"
                                    label="Vare*"
                                    type="text"
                                    placeholder="Skriv inn vare"
                                    value={orderDetails.orderProduct}
                                    onChange={handleInputChange}
                                    name="orderProduct"
                                    error={orderDetails.errors.orderProduct}
                                    labelSize={{xs: 4, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 5 }}
                                />


                                <CustomFormInput
                                    controlId="orderQuantity"
                                    label="Antall*"
                                    type="number"
                                    placeholder="Skriv inn antall"
                                    value={orderDetails.orderQuantity}
                                    onChange={handleInputChange}
                                    name="orderQuantity"
                                    error={orderDetails.errors.orderQuantity}
                                    labelSize={{xs: 4, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 5 }}
                                />

                                <Col className={"mt-5 mb-2"}>
                                    <DeliveryMethodSelect
                                        selectedMethod={orderDetails.deliveryMethod}
                                        onChange={handleDeliveryMethodChange}
                                    />
                                </Col>



                                <CustomFormInput
                                    controlId="price"
                                    label="Pris inkl. mva*"
                                    type="number"
                                    placeholder="Skriv inn pris"
                                    value={orderDetails.price}
                                    onChange={(e) => handleInputChange({ target: { name: 'price', value: e.target.value }})}
                                    name="price"
                                    error={orderDetails.errors.price}
                                    labelSize={{xs: 5, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 4, lg: 6 }}
                                    appendText="kr"
                                />

                                <CustomFormInput
                                    controlId="customerAddress"
                                    label="Adresse*"
                                    type="text"
                                    placeholder="Skriv inn adresse"
                                    value={orderDetails.customerAddress}
                                    onChange={handleInputChange}
                                    name="customerAddress"
                                    error={orderDetails.errors.customerAddress}
                                    labelSize={{xs: 5, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 6 }}
                                />

                                <CustomFormInput
                                    controlId="customerHouseNumber"
                                    label="Husnummer*"
                                    type="text"
                                    placeholder="Skriv inn husnummer"
                                    value={orderDetails.customerHouseNumber}
                                    onChange={handleInputChange}
                                    name="customerHouseNumber"
                                    error={orderDetails.errors.customerHouseNumber}
                                    labelSize={{xs: 5, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 6 }}
                                />

                                <CustomFormInput
                                    controlId="customerPostNumber"
                                    label="Postnummer*"
                                    type="number"
                                    placeholder="Skriv inn postnummer"
                                    value={orderDetails.customerPostNumber}
                                    onChange={handleInputChange}
                                    name="customerPostNumber"
                                    error={orderDetails.errors.customerPostNumber}
                                    labelSize={{xs: 5, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 6 }}
                                />


                                <CustomFormInput
                                    controlId="customerLocation"
                                    label="Sted*"
                                    type="text"
                                    placeholder="Skriv inn sted"
                                    value={orderDetails.customerLocation}
                                    onChange={handleInputChange}
                                    name="customerLocation"
                                    error={orderDetails.errors.customerLocation}
                                    labelSize={{xs: 5, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 6 }}
                                />


                                <Row className="align-items-center">
                                    <Col xs={3}>
                                        <Form.Label>Status</Form.Label>
                                    </Col>
                                    <Col xs={9} md={6}>
                                        <ToggleButtonGroup
                                            type="radio"
                                            name="status"
                                            value={orderDetails.status}
                                            onChange={(newStatus) => handleStatusChange(newStatus)} // Directly use newStatus
                                        >
                                            <ToggleButton id="tbg-btn-1" value="ferdig" variant="outline-success">
                                                Ferdig
                                            </ToggleButton>
                                            <ToggleButton id="tbg-btn-2" value="ikke ferdig" variant="outline-danger">
                                                Ikke ferdig
                                            </ToggleButton>
                                        </ToggleButtonGroup>
                                    </Col>
                                </Row>
                                <Row className="align-items-center mt-3">
                                    <Col xs={3}>
                                        <Form.Label>Betalt</Form.Label>
                                    </Col>
                                    <Col xs={6}>
                                        <Form.Check
                                            type="checkbox"
                                            id="check-payment"
                                            label="Ja"
                                            checked={orderDetails.paid}
                                            onChange={(e) => handlePaymentChange(e.target.checked)}
                                        />
                                    </Col>
                                </Row>
                            </Form>
                        </Col>
                    </Row>


                    {orderDetails.userErrors && (
                        <div className="alert alert-danger" role="alert">
                            {orderDetails.userErrors}
                        </div>
                    )}

                    <Row className="justify-content-end mt-5">
                        <Col xs={7} md={3} lg={3}> {/* xs="auto" ensures that the column only takes up as much space as the button needs */}
                            <GeneralBottom
                                variant="secondary"
                                text={isEditMode ? "Lagre endringer" : "Legg til ordre"}
                                className="btn-custom-submit-order"
                                onClick={handleSubmit}
                            />
                        </Col>

                        <Col xs={4} md={2} lg={1} className="me-4 mb-3">
                            <GeneralBottom
                                variant="neutral"
                                text="Tilbake"
                                className="btn-custom-cancel-order"
                                onClick={() => navigate('/produsent/order')}
                            />
                        </Col>

                        <Col xs={6} md={3} lg={2}>
                            <Button variant="outline-secondary" onClick={() => window.print()} className="btn-no-border">
                                Print ordre <FontAwesomeIcon icon={faPrint} />
                            </Button>
                        </Col>
                    </Row>

                </Card.Body>
            </Card>
        </div>
    );
}

export default OrderPage;
