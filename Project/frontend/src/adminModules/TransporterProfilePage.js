import {Card, Col, Form, Row, Tab, Tabs, ToggleButton, ToggleButtonGroup} from "react-bootstrap";
import Button from "react-bootstrap/Button";

import React, {useEffect, useState} from "react";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faPrint} from "@fortawesome/free-solid-svg-icons";
import "../adminModules/Orders/OrderPage.css"
import {useNavigate, useParams} from "react-router-dom";
import DeliveryMethodSelect from "./Storage/Components/DeliveryMethodSelect/DeliveryMethodSelect";
import CustomFormInput from "./Shared/Components/InputFields/CustomFormInput";
import ImageUpload from "./Storage/Components/ImageUpload/ImageUpload";
import Description from "./Storage/Components/Description/Description";
import GeneralBottom from "./Shared/Components/GeneralBottom/GeneralBottom";
import CustomTable from "./Shared/SingleComponents/TableComponent";
import "./TransporterProfilePage.css";

const mockOrderDetails = {
    '1': {
        transporterOrgNumber: '123456789',
        image: 'https://via.placeholder.com/150', // Placeholder image URL for demonstration
        transporterDescription: 'This is a sample description for the transporter company.',
        transporterCompany: 'Sample Transporter Co.',
        transporterName: 'John Doe',
        transporterEmail: 'john.doe@example.com',
        transporterPhone: '+1234567890',
        transporterAddress: '123 Main St, City, Country',
        transporterLocation: 'Cityville',
        emailNotifications: true,
        errors: {},
        userErrors: ''
    },
    '2': {
        transporterOrgNumber: '875738274',
        image: null,
        transporterDescription: 'Dette er test beskrivelse id 2',
        transporterCompany: 'Company ID 2',
        transporterName: 'Transporter navn ID 2',
        transporterEmail: 'Transporter email 2',
        transporterPhone: '+56 589452390',
        transporterAddress: '222 Cali, City, State',
        transporterLocation: 'Cali State',
        emailNotifications: false,
        errors: {},
        userErrors: ''
    },
    // Add more mock orders as needed...
};

function TransporterProfilePage( ) {

    const { transporterId } = useParams();

    const navigate = useNavigate();


    const [activeTab, setActiveTab] = useState('generalInfo');

    const [isEditMode, setIsEditMode] = useState(false); //If the product is editing instead of adding

    const [transporterDetails, setTransporterDetails] = useState({
        transporterOrgNumber: '',
        image: null,
        transporterDescription: '',
        transporterCompany: '',
        transporterName: '',
        transporterEmail: '',
        transporterPhone: '',
        transporterAddress: '',
        transporterLocation: '',
        emailNotifications: false,
        errors: {},
        userErrors: ''
    });

    useEffect(() => {
        if (transporterId) {
            setIsEditMode(true);

            //fetchTransporterDetails(transporterId); Når kobler til backend

            const details = mockOrderDetails[transporterId];
            if (details) {
                setTransporterDetails(details);
            } else {
                console.error("Transporter details not found for transporterId:", transporterDetails);
                // Handle order not found
            }

        } else {
            setIsEditMode(false);
            // Set orderDetails to default values for a new order
        }
    }, [transporterId]);



    const fetchTransporterDetails = async (transporterId) => {
        try {
            const response = await fetch(`/api/transporters/${transporterId}`);
            if (!response.ok) {
                throw new Error('Failed to fetch transporter details');
            }
            const details = await response.json();
            setTransporterDetails(details);
        } catch (error) {
            console.error("Fetching transporter details failed:", error);
            // Handle error state appropriately
        }
    };



    const handleImageSelected = (imageFile) => {
        setTransporterDetails((prevDetails) => ({
            ...prevDetails,
            image: imageFile,
        }));
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;

        // Validate the input
        const error = validateInput(name, value);

        // Set input value and error message
        setTransporterDetails(prevDetails => ({
            ...prevDetails,
            [name]: value,
            errors: {
                ...prevDetails.errors,
                [name]: error,
            }
        }));
    };

    const handleEmailNotificationsChange = (isEnabled) => {
        setTransporterDetails(prevDetails => ({
            ...prevDetails,
            emailNotifications: isEnabled,
        }));
    };

    const handleDescriptionChange = (newDescription) => {
        setTransporterDetails(prevDetails => ({
            ...prevDetails,
            description: newDescription
        }));
    };

    const upcomingAssignments = [
        { orderId: '#78954', transporter: 'Jan Tore Kjær', to: 'Storgaten 25', from: 'Kølleklev 21', status: 'Aktiv' },
        { orderId: '#78954', transporter: 'Jan Tore Kjær', to: 'Storgaten 25', from: 'Kølleklev 21', status: 'ikke aktiv' },
        // ... more assignment objects
    ];

    const columns = [
        { header: 'OrdreId', accessor: 'orderId' },
        { header: 'Transportør', accessor: 'transporter' },
        { header: 'Til', accessor: 'to' },
        { header: 'Fra', accessor: 'from' },
        { header: 'Status', accessor: 'status', render: (item) => (
                <span className={`status-badge ${item.status === 'Aktiv' ? 'status-active' : 'status-inactive'}`}>
      {item.status}
    </span>
            )
        },
    ];



    const handleSubmit = async () => {
        // Validate required fields before submission
        const errors = {};
        // Define the fields required for the transporter
        const requiredFields = [
            'transporterCompany', 'transporterName', 'transporterEmail', 'transporterPhone',
            'transporterAddress', 'transporterLocation', 'emailNotifications'
        ];

        requiredFields.forEach(field => {
            const error = validateInput(field, transporterDetails[field]);
            if (error) errors[field] = error;
        });

        // Update the state with any validation errors
        setTransporterDetails(prevDetails => ({
            ...prevDetails,
            errors: errors
        }));

        if (Object.keys(errors).length === 0) {
            const method = isEditMode ? 'PUT' : 'POST';
            const endpoint = `${process.env.REACT_APP_API_URL}/transporters${isEditMode ? `/${transporterId}` : ''}`;

            // Construct the data object you're going to send
            const payload = {
                transporterOrgNumber: '',
                image: transporterDetails.image,
                transporterDescription: '',
                transporterCompany: transporterDetails.transporterCompany,
                transporterName: transporterDetails.transporterName,
                transporterEmail: transporterDetails.transporterEmail,
                transporterPhone: transporterDetails.transporterPhone,
                transporterAddress: transporterDetails.transporterAddress,
                transporterLocation: transporterDetails.transporterLocation,
                emailNotifications: transporterDetails.emailNotifications,
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
            setTransporterDetails(prevDetails => ({
                ...prevDetails,
                userErrors: 'Det skjedde en feil. Vennligst prøv igjen senere.'
            }));

        }
    };


    const validateInput = (name, value) => {
        if (!value || !value.trim()) {
            return `${name} er obligatorisk.`;
        }
        return '';
    };

    const handleDeleteTransporter = async () => {
        // Confirm deletion with the user before proceeding
        if (window.confirm('Er du sikker på at du vil slette denne transportøren?')) {
            try {
                const endpoint = `${process.env.REACT_APP_API_URL}/transporters/${transporterDetails.id}`;
                const response = await fetch(endpoint, {
                    method: 'DELETE',
                    headers: {
                        // Include authorization header if needed
                         // Assuming 'token' is your authentication token
                    },
                });

                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }

                console.log('Transporter deleted successfully');
                // Redirect to transporters list or update UI accordingly
                // For example:
                // navigate('/transporters'); // Assuming you're using react-router for navigation
            } catch (error) {
                console.error('There was a problem with the fetch operation:', error);
                // Handle error state appropriately
                // For example, set an error state and display it in the UI
            }
        }
    };


    return (
        <div className="container mt-4">
            <Card>
                <Card.Header as="h3" className="d-flex justify-content-between align-items-center">
                    <span>{isEditMode ? `Endre transporter: ${transporterDetails.transporterCompany}` : "Legg til transporter"}</span>
                    {isEditMode && (
                        <Button variant="outline-danger" onClick={handleDeleteTransporter}>
                            Slett transportør
                        </Button>
                    )}
                </Card.Header>
                <Card.Body>
                    <Tabs activeKey={activeTab} onSelect={(k) => setActiveTab(k)} id="product-info-tabs" className="mb-3">
                        <Tab eventKey="generalInfo" title={<span className={`tab-custom ${activeTab === 'generalInfo' ? 'active' : 'inactive'}`}>Generell info</span>}>
                            {/* Content for General Info */}
                        </Tab>
                        <Tab eventKey="statisticspickup" title={<span className={`tab-custom ${activeTab === 'statisticspickup' ? 'active' : 'inactive'}`}>Statestikk hentetider</span>}>
                            {/* Content for Customer Perspective */}
                        </Tab>
                        <Tab eventKey="tenderassignment" title={<span className={`tab-custom ${activeTab === 'tenderassignment' ? 'active' : 'inactive'}`}>Anbudsoppdrag</span>}>
                            {/* Content for Feedback */}
                        </Tab>
                        <Tab eventKey="mission" title={<span className={`tab-custom ${activeTab === 'mission' ? 'active' : 'inactive'}`}>Oppdrag</span>}>
                            {/* Content for Feedback */}
                        </Tab>
                    </Tabs>



                    {/* Rest of your product page content */}

                    <Row>
                        <Col md={12} className="d-flex justify-content-end text-muted">
                            <span>Org.nr.: {transporterDetails.transporterOrgNumber || "Tilordnes"}</span>
                        </Col>
                    </Row>


                    <Row>
                        {/* First column for image */}
                        <Col xs={12} md={12} lg={6} >

                            <ImageUpload onImageSelected={handleImageSelected} />

                            <Description onDescriptionChange={handleDescriptionChange} value={transporterDetails.transporterDescription} showSuggestions={false} />
                        </Col>



                        {/* Second column for product details form */}
                        <Col xs={12} md={12} lg={6}>

                            <Form>

                                <CustomFormInput
                                    controlId="transporterCompany"
                                    label="Leverandør*"
                                    type="text"
                                    placeholder="Skriv inn leverandør"
                                    value={transporterDetails.transporterCompany}
                                    onChange={handleInputChange}
                                    name="transporterCompany"
                                    error={transporterDetails.errors.transporterCompany}
                                    labelSize={{xs: 8, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 12, sm: 8, md: 6, lg: 5 }}
                                />

                                <CustomFormInput
                                    controlId="transporterName"
                                    label="Transportør*"
                                    type="text"
                                    placeholder="Skriv inn transportørnavn"
                                    value={transporterDetails.transporterName}
                                    onChange={handleInputChange}
                                    name="transporterName"
                                    error={transporterDetails.errors.transporterName}
                                    labelSize={{xs: 7, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 12, sm: 8, md: 6, lg: 5 }}
                                />


                                <CustomFormInput
                                    controlId="transporterEmail"
                                    label="Email*"
                                    type="email"
                                    placeholder="Skriv inn email"
                                    value={transporterDetails.transporterEmail}
                                    onChange={handleInputChange}
                                    name="transporterEmail"
                                    error={transporterDetails.errors.transporterEmail}
                                    labelSize={{xs: 4, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 5 }}
                                />

                                <CustomFormInput
                                    controlId="transporterPhone"
                                    label="Tlf nr.*"
                                    type="num"
                                    placeholder="Skriv inn tlf."
                                    value={transporterDetails.transporterPhone}
                                    onChange={handleInputChange}
                                    name="transporterPhone"
                                    error={transporterDetails.errors.transporterPhone}
                                    labelSize={{xs: 4, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 5 }}
                                />


                                <CustomFormInput
                                    controlId="transporterAddress"
                                    label="Adresse*"
                                    type="text"
                                    placeholder="Skriv inn adresse"
                                    value={transporterDetails.transporterAddress}
                                    onChange={handleInputChange}
                                    name="transporterAddress"
                                    error={transporterDetails.errors.transporterAddress}
                                    labelSize={{xs: 4, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 5 }}
                                />

                                <CustomFormInput
                                    controlId="transporterLocation"
                                    label="Område*"
                                    type="text"
                                    placeholder="Skriv inn område"
                                    value={transporterDetails.transporterLocation}
                                    onChange={handleInputChange}
                                    name="transporterLocation"
                                    error={transporterDetails.errors.transporterLocation}
                                    labelSize={{xs: 4, sm: 4, md: 3, lg: 4 }}
                                    inputSize={{xs: 7, sm: 8, md: 6, lg: 5 }}
                                />

                                <Form.Check
                                    type="checkbox"
                                    id="email-notifications"
                                    label="Aktiver epost om oppdrag"
                                    checked={transporterDetails.emailNotifications}
                                    onChange={(e) => handleEmailNotificationsChange(e.target.checked)}
                                />


                                <Row className={"mt-5"}>
                                    <CustomTable data={upcomingAssignments} columns={columns} />
                                </Row>


                            </Form>

                        </Col>
                    </Row>


                    {transporterDetails.userErrors && (
                        <div className="alert alert-danger" role="alert">
                            {transporterDetails.userErrors}
                        </div>
                    )}

                    <Row className="justify-content-end mt-5">
                        <Col xs={7} md={3} lg={3}> {/* xs="auto" ensures that the column only takes up as much space as the button needs */}
                            <GeneralBottom
                                variant="secondary"
                                text={isEditMode ? "Lagre endringer" : "Legg til transportør"}
                                className="btn-custom-submit-order"
                                onClick={handleSubmit}
                            />
                        </Col>

                        <Col xs={4} md={2} lg={1} className="me-4 mb-3">
                            <GeneralBottom
                                variant="neutral"
                                text="Tilbake"
                                className="btn-custom-cancel-order"
                                onClick={() => navigate('/admin/transporter')}
                            />
                        </Col>
                    </Row>

                </Card.Body>
            </Card>
        </div>
    );
}

export default TransporterProfilePage;
