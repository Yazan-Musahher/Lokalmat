import React, { useState } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

import GeneralBottom from "../../../Shared/Components/GeneralBottom/GeneralBottom";

const DiscountModal = ({ show, handleClose, handleSave, originalPrice, discountValue, setDiscountValue }) => {
    const [dateRange, setDateRange] = useState([null, null]);
    const [startDate, endDate] = dateRange;


    const [localDiscountValue, setLocalDiscountValue] = useState('');

    const calculateNewPrice = () => {
        // Convert discountValue to a number and ensure it's not NaN
        const discountAmount = Number(discountValue) || 0;
        return Math.max(originalPrice - discountAmount, 0); // Ensure the new price can't go below 0
    };

    const [testInput, setTestInput] = useState('');

    return (
        <Modal show={show} onHide={handleClose}>


            <Modal.Header closeButton>
                <Modal.Title>Velg rabatt</Modal.Title>
            </Modal.Header>
            <Modal.Body>


                <Form>
                    <Form.Group className="mb-3">
                        <Form.Label>Original pris:</Form.Label>
                        <Form.Control type="text" readOnly value={`${originalPrice} kr`} />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Rabatt (i kr):</Form.Label>
                        <Form.Control
                            type="number"
                            placeholder="Angi rabatt i kr"
                            value={localDiscountValue}
                            onChange={(e) => setLocalDiscountValue(e.target.value)} // Set the local state
                        />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Ny pris:</Form.Label>
                        <Form.Control type="text" readOnly value={`${calculateNewPrice()} kr`} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label>Velg varighet for rabatt i kalenderen:</Form.Label>
                        <DatePicker
                            selectsRange={true}
                            startDate={startDate}
                            endDate={endDate}
                            onChange={(update) => {
                                setDateRange(update);
                            }}
                            withPortal
                            inline
                        />
                    </Form.Group>
                    {/* Add other inputs and checkboxes as needed */}



                </Form>
            </Modal.Body>
            <Modal.Footer>
                <GeneralBottom
                    variant="secondary"
                    text="Lagre"
                    className="btn-custom-submit-discount"
                    onClick={() => {
                        handleSave(discountValue, dateRange); // Pass discountValue and dateRange to the save function
                        handleClose(); // Close the modal after saving
                    }}
                />

                <GeneralBottom
                    variant="neutral"
                    text="Avbryt"
                    className="btn-custom-cancel-discount"
                    onClick={handleClose}
                />

            </Modal.Footer>
        </Modal>
    );
};

export default DiscountModal;
