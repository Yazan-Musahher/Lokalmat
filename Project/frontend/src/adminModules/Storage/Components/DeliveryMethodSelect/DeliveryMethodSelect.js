import React from 'react';
import { Form, Row, Col, Button } from 'react-bootstrap';
import { faTruck } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

const DeliveryMethodSelect = ({ onChange, selectedMethod }) => {
    // Placeholder for delivery methods
    const deliveryMethods = [
        { id: 'posten', name: 'Posten' },
        { id: 'bring', name: 'Bring' },
        { id: 'klinkkhent', name: 'Klikk & Hent' }
        // Add more delivery methods here
    ];

    return (
        <Form.Group as={Row} controlId="delivery-method">
            <Col sm={7} md={6} lg={7} className={"mb-2 mb-md-3 mb-sm-4"}>

                    <Form.Control as="select" value={selectedMethod} onChange={onChange} className="form-select">
                        <option value="">Velg leveransemetode</option> {/* Add a default option */}
                        {deliveryMethods.map(method => (
                            <option key={method.id} value={method.id}>{method.name}</option>
                        ))}
                        <FontAwesomeIcon icon="chevron-down" /> {/* Use the appropriate icon */}
                    </Form.Control>

            </Col>
            <Col sm={5} md={6} lg={5} className="d-flex justify-content-end mb-2 mb-sm-4">
                <Button variant="outline-secondary">
                    <FontAwesomeIcon icon={faTruck} className="me-2" />
                    Leveransemetoder
                </Button>
            </Col>
        </Form.Group>
    );
};

export default DeliveryMethodSelect;
