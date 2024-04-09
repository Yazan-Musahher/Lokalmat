import React from 'react';
import { Row, Col, Button } from 'react-bootstrap';
import "../CSS/ButtonsColorComponent.css";
const HeaderButtons = ({ title, buttons }) => {
    return (
        <Row className="mb-2 align-items-center">
            <Col lg={4} sm={12} >
                <h1>{title}</h1>
            </Col>
            <Col lg={8} sm={12}  className="text-md-end text-start">
                {buttons.map((button, index) => (
                    <Button
                        key={index}
                        variant={button.variant}
                        className={`${button.className} mt-md-0 mb-2 `}
                        onClick={button.onClick}
                    >
                        {button.text}
                    </Button>
                ))}
            </Col>
        </Row>
    );

};

export default HeaderButtons;