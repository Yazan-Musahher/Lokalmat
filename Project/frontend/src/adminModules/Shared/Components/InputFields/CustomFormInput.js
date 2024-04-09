import React from 'react';
import { Form, Row, Col } from 'react-bootstrap';

const CustomFormInput = ({
                             controlId,
                             label,
                             type,
                             placeholder,
                             value,
                             onChange,
                             name,
                             error,
                             labelSize,
                             inputSize,
                             appendText
                         }) => {

    return (
        <Form.Group as={Row} controlId={controlId} className="mb-4 align-items-center">
            <Form.Label column {...labelSize}>
                {label}
            </Form.Label>
            <Col {...inputSize}>
                <div className="d-flex align-items-center">
                <Form.Control
                    type={type}
                    placeholder={placeholder}
                    value={value}
                    onChange={onChange}
                    name={name}
                    className="border-0 border-bottom"
                    isInvalid={!!error}
                />
                {appendText && <span className="ms-2">{appendText}</span>}
                </div>
                <Form.Control.Feedback type="invalid">
                    {error}
                </Form.Control.Feedback>
            </Col>
        </Form.Group>
    );
};


export default CustomFormInput;
