import React, { useState } from 'react';
import { Card, Button, Row, Col, Form, Alert,Toast, ToastContainer } from 'react-bootstrap';
import GeneralBottom from "../Shared/Components/GeneralBottom/GeneralBottom";
import './ProductPage.css';
import Description from "./Components/Description/Description";
import DeliveryMethodSelect from "./Components/DeliveryMethodSelect/DeliveryMethodSelect";
import CustomFormInput from "../Shared/Components/InputFields/CustomFormInput";
import PriceAndDiscountSection from "./Components/PriceAndDiscountSection/PriceAndDiscountSection";
import { useNavigate, useParams } from "react-router-dom";
import { useAuth } from '../../contexts/AuthContext';
import { API_BASE_URL, PRODUCT_QUERY_BY_MANUFACTURER_URL } from '../../credentials';

function ProductPage() {
    const navigate = useNavigate();
    const { productId } = useParams();
    const { user } = useAuth(); //Retrieve user details from AuthContext

    const [activeTab, setActiveTab] = useState('generalInfo');
    const [showDiscountModal, setShowDiscountModal] = useState(false);
    const [discountValue, setDiscountValue] = useState('');

    const [showToast, setShowToast] = useState(false);
    const [isEditMode, setIsEditMode] = useState(false); //If the product is editing instead of adding
    const [productDetails, setProductDetails] = useState({
        name: '',
        category: '',
        description: '',
        imageUrl: '',
        stock: 0,
        city: '',
        postalCode: 0,
        price: 0,
        popularity: 0,
        rating: 0,
        ratingCount: 0,
        manufacturerId:'' 
    });

    const [successMessage, setSuccessMessage] = useState('');

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        const isNumeric = ['stock', 'postalCode', 'price', 'popularity', 'rating', 'ratingCount'].includes(name);
        setProductDetails(prevDetails => ({
            ...prevDetails,
            [name]: isNumeric ? parseFloat(value) || 0 : value // Use parseFloat for price and parseInt for integers
        }));
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        const manufacturerEmail = localStorage.getItem('email');

        if (!manufacturerEmail) {
            alert("Manufacturer email is not available. Please login or check your settings.");
            return;
        }

        const url = `${API_BASE_URL}${PRODUCT_QUERY_BY_MANUFACTURER_URL.replace('{manufacturerEmail}', encodeURIComponent(manufacturerEmail))}`;

        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(productDetails)
            });

            if (!response.ok) {
                const errorData = await response.json();
                console.error('HTTP Error Response:', errorData);
                throw new Error(`HTTP error! status: ${response.status} - ${errorData.message}`);
            }

            const result = await response.json();
            console.log('Product created:', result);
            setShowToast(true);
            setTimeout(() => {
                navigate('/produsent/stock'); // Redirect after successful creation
                setShowToast(false); // Reset the toast visibility
            }, 3000); // Delay the navigation to keep the toast visible
        } catch (error) {
            console.error('Failed to create product:', error);
            alert('Failed to create product. Please try again.');
        }
    };

    return (
        <div className="container mt-4">
            <ToastContainer className="p-3" position="top-end">
                <Toast onClose={() => setShowToast(false)} show={showToast} delay={3000} autohide bg="success">
                    <Toast.Header>
                        <strong className="me-auto">vellykket</strong>
                        <small>Akkurat nå</small>
                    </Toast.Header>
                    <Toast.Body>Produktet ble opprettet!</Toast.Body>
                </Toast>
            </ToastContainer>
            <Card>
                <Card.Header as="h3" className="d-flex justify-content-between align-items-center">
                    <span>{isEditMode ? `Endre produkt: ${productDetails.name}` : "Legg til produkt"}</span>
                    {isEditMode && (
                        <Button variant="outline-danger">
                            Slett ordre
                        </Button>
                    )}
                </Card.Header>
                <Card.Body>
                {successMessage && <Alert variant="success">{successMessage}</Alert>}
                    <Form onSubmit={handleSubmit}>
                        <Row>
                            <Col xs={12} md={12} lg={6}>
                                <CustomFormInput
                                    controlId="name"
                                    label="Produktnavn*"
                                    type="text"
                                    placeholder="Skriv inn produktnavn"
                                    value={productDetails.name}
                                    name="name"
                                    onChange={handleInputChange}
                                />
                                <CustomFormInput
                                    controlId="category"
                                    label="Kategori*"
                                    type="text"
                                    placeholder="Skriv inn kategori"
                                    value={productDetails.category}
                                    name="category"
                                    onChange={handleInputChange}
                                />
                                <CustomFormInput
                                    controlId="stock"
                                    label="Antall*"
                                    type="number"
                                    placeholder="Skriv inn antall"
                                    value={productDetails.stock}
                                    name="stock"
                                    onChange={handleInputChange}
                                />
                                <CustomFormInput
                                    controlId="city"
                                    label="By*"
                                    type="text"
                                    placeholder="Skriv inn By"
                                    value={productDetails.city}
                                    name="city"
                                    onChange={handleInputChange}
                                />
                                <CustomFormInput
                                    controlId="postalcode"
                                    label="Postnummer*"
                                    type="text"
                                    placeholder="Skriv inn postnummer"
                                    value={productDetails.postalCode}
                                    name="postalCode"
                                    onChange={handleInputChange}
                                />
                                <CustomFormInput
                                    controlId="price"
                                    label="Pris*"
                                    type="number"
                                    placeholder="Skriv inn pris"
                                    value={productDetails.price}
                                    name="price"
                                    onChange={handleInputChange}
                                />
                                <CustomFormInput
                                    controlId="image"
                                    label="Bilde*"
                                    type="text"
                                    placeholder="Lim inn link av bilde"
                                    value={productDetails.imageUrl}
                                    name="imageUrl"
                                    onChange={handleInputChange}
                                />
                                <PriceAndDiscountSection
                                    price={productDetails.price}
                                    onAddDiscount={() => setShowDiscountModal(true)}
                                    showDiscountModal={showDiscountModal}
                                    handleCloseDiscountModal={() => setShowDiscountModal(false)}
                                    discountValue={discountValue}
                                    setDiscountValue={setDiscountValue}
                                />
                                <DeliveryMethodSelect
                                    selectedMethod={productDetails.deliveryMethod}
                                    onChange={(e) => setProductDetails(prevDetails => ({
                                        ...prevDetails,
                                        deliveryMethod: e.target.value,
                                    }))}
                                />
                                <Description
                                    onDescriptionChange={(newDescription) => setProductDetails(prevDetails => ({
                                        ...prevDetails,
                                        description: newDescription
                                    }))}
                                    value={productDetails.description}
                                />
                                <Button type="submit" variant="secondary" className="btn-custom-submit-product">
                                    {isEditMode ? "Lagre endringer" : "Legg til produkt"}
                                </Button>
                            </Col>
                        </Row>
                    </Form>
                </Card.Body>
            </Card>
        </div>
    );
}

export default ProductPage;
