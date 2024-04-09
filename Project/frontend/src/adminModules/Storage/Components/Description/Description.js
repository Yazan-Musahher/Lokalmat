import React, {useEffect, useState} from 'react';
import { Form, Row, Col } from 'react-bootstrap';
import './Description.css';

const Description = ({ onDescriptionChange, value, showSuggestions = true}) => {

    const [description, setDescription] = useState(value || '');
    const suggestions = [
        "Lorem ipsum hfilokius hipsan1...",
        "Lorem ipsum hfilokius hipsan2...",
        "Lorem ipsum hfilokius hipsan3...",
        // Add more AI-generated suggestions as needed
    ];

    useEffect(() => {
        // Update internal state if the prop value changes
        setDescription(value || '');
    }, [value]);

    const handleSuggestionClick = (suggestion) => {
        setDescription(suggestion);
        onDescriptionChange(suggestion); // Update parent component's state
    };

    const handleChange = (e) => {
        const newDescription = e.target.value;
        setDescription(newDescription);
        onDescriptionChange(newDescription); // Update parent component's state
    };

    return (
        <>
        <Form.Label column sm={6}>Beskrivelse:</Form.Label>
        <Row className="mb-3">
            <Col md={showSuggestions ? 6 : 10} >
                <Form.Group as={Row} controlId="product-description" className="mb-3 align-items-center">
                    <Form.Control as="textarea" rows={8} placeholder="Skriv her:" value={description} onChange={handleChange} />
                    <Form.Text className="text-muted">
                        {description.length}/250
                    </Form.Text>
                </Form.Group>
            </Col>
            {showSuggestions && (
            <Col md={6}>
                <div className="ms-2">
                    <p>Forslag til varebeskrivelse av KI:</p>
                    <div className="list-group">
                        {suggestions.map((suggestion, index) => (
                            <button
                                key={index}
                                type="button"
                                className="list-group-item list-group-item-action suggestion-item"
                                onClick={() => handleSuggestionClick(suggestion)}
                            >
                                {suggestion}
                            </button>
                        ))}
                    </div>
                </div>
            </Col>
            )}
        </Row>
        </>
    );
};

export default Description;
