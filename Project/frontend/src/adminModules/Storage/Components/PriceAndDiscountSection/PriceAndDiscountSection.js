import React from 'react';
import { Row, Col, Form, Button } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faHeart } from '@fortawesome/free-solid-svg-icons';
import DiscountModal from "./DiscountModal";

const PriceAndDiscountSection = ({ price,
                                     setPrice,
                                     onAddDiscount,
                                     priceError,
                                     showDiscountModal,
                                     handleCloseDiscountModal,
                                     discountValue,
                                     setDiscountValue,
                                     showDiscountButton = true,
                                     includeVAT = false}) => {

    const numericDiscountValue = Number(discountValue) || 0;
    const discountedPrice = price - numericDiscountValue;

    return (
        <>
        <Row className="align-items-center mb-3 g-2 mb-sm-3">
            {/* Price input should take full width on small screens and specified width on larger screens */}
            <Col  xs={10} sm={9} md={6} lg={7} xl={6} className="d-flex justify-content-start align-items-center mb-2 mb-md-3 mb-sm-3">
                <Form.Label className="me-3">Pris*</Form.Label>

                <Form.Control
                    type="number"
                    placeholder="Skriv inn pris"
                    value={price}
                    onChange={(e) => setPrice(e.target.value)}
                    className="border-0 border-bottom"
                    isInvalid={!!priceError}
                />
                <span className="input-group-text border-0 bg-transparent">kr{includeVAT ? ' inkl. mva' : ''}</span>

                {numericDiscountValue > 0 && (
                    <Col xs={5} sm={5} md={4} lg={4} xl={4} className="d-flex align-items-center">
                        <div>
                            <span style={{ textDecoration: 'line-through', marginRight: '10px' }}>
                                {price}
                            </span>
                            <span>{discountedPrice} kr</span>
                        </div>
                    </Col>
                )}
            </Col>
            {/* Show discounted price next to the input if a discount is applied */}



            {showDiscountButton && (
            <Col xs={12} sm={12} md={6} lg={5} xl={6} className="d-flex justify-content-end mt-4 mb-sm-4">
                <Button variant="outline-success" onClick={onAddDiscount}>
                    <FontAwesomeIcon icon={faHeart} className="me-2" />
                    Legg til rabatt
                </Button>
            </Col>
            )}

            {showDiscountModal && (
                <DiscountModal
                    show={showDiscountModal}
                    handleClose={handleCloseDiscountModal}
                    originalPrice={price}
                    discountValue={discountValue}
                    setDiscountValue={setDiscountValue}

                    handleSave={() => {/* logic to save the discounted price */}}
                />
            )}

            {priceError && (
                <Row>
                    <Col className="mt-0 mb-5">
                        <div className="text-danger">{/*{priceError}*/"Dette feltet er obligatorisk"}</div>
                    </Col>
                </Row>
            )}


        </Row>

        </>
    );
};

export default PriceAndDiscountSection;
