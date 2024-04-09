import React, {useEffect, useState} from 'react';
import {Card, Button, Tabs, Tab, Row, Col, Form, Image, Container} from 'react-bootstrap';
import GeneralBottom from "../Shared/Components/GeneralBottom/GeneralBottom";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faHeart} from "@fortawesome/free-solid-svg-icons";
import './ProductPage.css';
import Description from "./Components/Description/Description";
import DeliveryMethodSelect from "./Components/DeliveryMethodSelect/DeliveryMethodSelect";
import CustomFormInput from "../Shared/Components/InputFields/CustomFormInput";
import PriceAndDiscountSection from "./Components/PriceAndDiscountSection/PriceAndDiscountSection";
import ImageUpload from "./Components/ImageUpload/ImageUpload";
import {useNavigate, useParams} from "react-router-dom";


const mockOrderDetails = {
    '1': {
        ratings: {
            average: 3,
            count: 4,
        },
        image: null,
        productName: 'Gulrot',
        productCategory: 'Grønnsak',
        productQuantity: '124',
        price: '7.99',
        discountApplied: false,
        discount: '',
        deliveryMethod: 'bring',
        description: 'Veldig gode gulrøtter.',
        errors: {},
        userErrors: ''
    },
    '2': {
        ratings: {
            average: 5,
            count: 15,
        },
        image: null,
        productName: 'Eple',
        productCategory: 'Frukt',
        productQuantity: '450',
        price: '3.99',
        discountApplied: false,
        discount: '',
        deliveryMethod: 'posten',
        description: 'Dette er veldig gode epler',
        errors: {},
        userErrors: ''
    },
    // Add more mock orders as needed...
};




function ProductPage() {

    const navigate = useNavigate();

    const { productId } = useParams();

    const [activeTab, setActiveTab] = useState('generalInfo');
    const [showDiscountModal, setShowDiscountModal] = useState(false);
    const [discountValue, setDiscountValue] = useState('');

    const [isEditMode, setIsEditMode] = useState(false); //If the product is editing instead of adding

    const [productDetails, setProductDetails] = useState({
        ratings: {
            average: 0,
            count: 0,
        },
        image: null,
        productName: '',
        productCategory: '',
        productQuantity: '',
        price: '',
        discountApplied: false,
        discount: '',
        deliveryMethod: '',
        description: '',
        errors: {},
        userErrors: ''
    });

    useEffect(() => {
        if (productId) {
            setIsEditMode(true);

            //fetchProductData(productId);

            const details = mockOrderDetails[productId];
            if (details) {
                setProductDetails(details);
            } else {
                console.error("Order details not found for orderId:", productDetails);
                // Handle order not found
            }

        } else {
            setIsEditMode(false);
            // Set orderDetails to default values for a new order
        }
    }, [productId]);


    const fetchProductData = async (productId) => {
        try {
            const response = await fetch(`api/products/${productId}`);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data = await response.json();
            setProductDetails({ ...data, errors: {} });
        } catch (error) {
            console.error("Could not fetch product:", error);
        }
    };




    const handleImageSelected = (imageFile) => {
        setProductDetails((prevDetails) => ({
            ...prevDetails,
            image: imageFile,
        }));
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;

        // Validate the input
        const error = validateInput(name, value);

        // Set input value and error message
        setProductDetails(prevDetails => ({
            ...prevDetails,
            [name]: value,
            errors: {
                ...prevDetails.errors,
                [name]: error,
            }
        }));
    };


    // Function to open the modal
    const handleAddDiscount = () => {
        // Here you can pass the current price to the modal, or handle discount logic

        setShowDiscountModal(true);
    };

    // Function to close the modal
    const handleCloseDiscountModal = () => {
        setShowDiscountModal(false);
    };


    const handleDeliveryMethodChange = (e) => {
        const newDeliveryMethod = e.target.value;
        setProductDetails(prevDetails => ({
            ...prevDetails,
            deliveryMethod: newDeliveryMethod,
        }));
    };

    const handleDescriptionChange = (newDescription) => {
        setProductDetails(prevDetails => ({
            ...prevDetails,
            description: newDescription
        }));
    };


    const handleSubmit = async () => {
        // Validate required fields before submission
        const errors = {};
        const requiredFields = ['productName', 'productCategory', 'productQuantity', 'price'];

        requiredFields.forEach(field => {
            const error = validateInput(field, productDetails[field]);
            if (error) {
                errors[field] = error;
            }
        });

        setProductDetails(prevDetails => ({
            ...prevDetails,
            errors: errors
        }));

        if (Object.keys(errors).length === 0) {
            console.log('Form data is valid. Preparing to submit:', productDetails);

            const method = isEditMode ? 'PUT' : 'POST';
            const apiEndpoint = isEditMode ? `${process.env.REACT_APP_API_URL}/products/${productId}` : `${process.env.REACT_APP_API_URL}/products`;

            // Construct the payload for the API call
            const payload = {
                productName: productDetails.productName,
                productCategory: productDetails.productCategory,
                productQuantity: productDetails.productQuantity,
                price: productDetails.price,
                discountApplied: productDetails.discountApplied,
                discount: productDetails.discountValue,
                deliveryMethod: productDetails.deliveryMethod,
                description: productDetails.description,
            };

            console.log('Sending the following data to the API:', payload);

            try {
                const response = await fetch(apiEndpoint, {
                    method: method,
                    headers: {
                        'Content-Type': 'application/json',
                        // Include authorization header if needed
                        // 'Authorization': `Bearer ${yourAuthToken}`
                    },
                    body: JSON.stringify(payload),
                });

                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }

                // Handle the response from the server
                const data = await response.json();
                console.log('Product added/updated successfully:', data);

                // Optional: Reset form or redirect user
                // setProductDetails({ ...initialFormState });

            } catch (error) {
                // Handle errors, such as displaying a notification to the user
                console.error('There was a problem with the fetch operation:', error);
                setProductDetails(prevDetails => ({
                    ...prevDetails,
                    errors: errors
                }));
            }
        } else {
            console.log('Form contains errors:', errors);
            setProductDetails(prevDetails => ({
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


    const handleDeleteProduct = async () => {
        // Confirm deletion with the user before proceeding
        if (window.confirm('Er du sikker på at du vil slette dette produktet?')) {
            try {
                const response = await fetch(`api/products/${productDetails.id}`, {
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
                    <span>{isEditMode ? `Endre produkt: ${productDetails.productName}` : "Legg til produkt"}</span>
                    {isEditMode && (
                        <Button variant="outline-danger" onClick={handleDeleteProduct}>
                        Slett ordre
                        </Button>
                    )}
                </Card.Header>
                <Card.Body>
                    <Tabs activeKey={activeTab} onSelect={(k) => setActiveTab(k)} id="product-info-tabs" className="mb-3">
                        <Tab eventKey="generalInfo" title={<span className={`tab-custom ${activeTab === 'generalInfo' ? 'active' : 'inactive'}`}>Generell info</span>}>
                            {/* Content for General Info */}
                        </Tab>
                        <Tab eventKey="customerPerspective" title={<span className={`tab-custom ${activeTab === 'customerPerspective' ? 'active' : 'inactive'}`}>Kunde perspektiv</span>}>
                            {/* Content for Customer Perspective */}
                        </Tab>
                        <Tab eventKey="feedback" title={<span className={`tab-custom ${activeTab === 'feedback' ? 'active' : 'inactive'}`}>Tilbakemeldinger</span>}>
                            {/* Content for Feedback */}
                        </Tab>
                    </Tabs>



                    {/* Rest of your product page content */}
                    <Row>
                        <Col md={12} className="d-flex justify-content-end mb-4">
                                <span className="me-2">
                                    {'★'.repeat(Math.floor(productDetails.ratings.average))}
                                    {'☆'.repeat(5 - Math.floor(productDetails.ratings.average))}
                                </span>
                            <span>{productDetails.ratings.count} tilbakemeldinger</span>
                        </Col>
                    </Row>

                    <Row>
                        {/* First column for image */}
                        <Col xs={12} md={12} lg={6} >

                            <ImageUpload onImageSelected={handleImageSelected} />

                        </Col>

                        {/* Second column for product details form */}
                        <Col xs={12} md={12} lg={6}>

                            <Form>
                            <CustomFormInput
                                controlId="productName"
                                label="Produktnavn*"
                                type="text"
                                placeholder="Skriv inn produktnavn"
                                value={productDetails.productName}
                                onChange={handleInputChange}
                                name="productName" // Important: Add 'name' prop for identifying the input
                                error={productDetails.errors.productName}
                                labelSize={{xs: 5, sm: 4, md: 3, lg: 4 }}
                                inputSize={{xs: 7, sm: 8, md: 6, lg: 6 }}
                            />


                            <CustomFormInput
                                controlId="productCategory"
                                label="Kategori*"
                                type="text"
                                placeholder="Skriv inn kategori"
                                value={productDetails.productCategory}
                                onChange={handleInputChange}
                                name="productCategory"
                                error={productDetails.errors.productCategory}
                                labelSize={{xs: 5, sm: 4, md: 3, lg: 4 }}
                                inputSize={{xs: 7, sm: 8, md: 6, lg: 6 }}
                            />

                            <CustomFormInput
                                controlId="productQuantity"
                                label="Antall*"
                                type="number"
                                placeholder="Skriv inn antall"
                                value={productDetails.productQuantity}
                                onChange={handleInputChange}
                                name="productQuantity"
                                error={productDetails.errors.productQuantity}
                                labelSize={{xs: 5, sm: 4, md: 3, lg: 4 }}
                                inputSize={{xs: 7, sm: 8, md: 6, lg: 6 }}
                            />



                                <PriceAndDiscountSection
                                    price={productDetails.price}
                                    setPrice={(newPrice) => handleInputChange({ target: { name: 'price', value: newPrice }})}
                                    onAddDiscount={handleAddDiscount}
                                    priceError={productDetails.errors.price}
                                    showDiscountModal={showDiscountModal}
                                    handleCloseDiscountModal={handleCloseDiscountModal}
                                    discountValue={discountValue}
                                    setDiscountValue={setDiscountValue}
                                />



                                <DeliveryMethodSelect
                                    selectedMethod={productDetails.deliveryMethod}
                                    onChange={handleDeliveryMethodChange}
                                />


                                <Description onDescriptionChange={handleDescriptionChange} value={productDetails.description} />



                            </Form>


                            <Row className="justify-content-end mt-5">
                                <Col xs={7} md={4} lg={6}>
                                <GeneralBottom
                                    variant="secondary"
                                    text={isEditMode ? "Lagre endringer" : "Legg til produkt"}
                                    className="btn-custom-submit-product"
                                    onClick={handleSubmit}
                                />
                                </Col>

                                    <Col xs={4} md={3} lg={4}>
                                <GeneralBottom
                                    variant="neutral"
                                    text="Tilbake"
                                    className="btn-custom-cancel-product"
                                    onClick={() => navigate('/admin/stock')}
                                />
                                    </Col>
                            </Row>
                        </Col>
                        {productDetails.userErrors && (
                            <div className="alert alert-danger" role="alert">
                                {productDetails.userErrors}
                            </div>
                        )}

                    </Row>
                </Card.Body>
            </Card>
        </div>
    );
}

export default ProductPage;
