import React from 'react';
import Card from 'react-bootstrap/Card';
import GeneralBottom from "../Shared/Components/GeneralBottom/GeneralBottom";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faCalendarDay} from "@fortawesome/free-solid-svg-icons";

function RekoRingCard({ title, date, content }) {
    return (
        <Card className="d-flex flex-column" style={{
            width: '18rem',
            textAlign: 'center',
            borderRadius: '20px',
            boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)'
        }}>
            <Card.Body className="d-flex flex-column">
                <Card.Title>{title}</Card.Title>
                {/* Calendar Icon */}
                <FontAwesomeIcon icon={faCalendarDay} size="3x" color="grey" className="my-2" />
                {/* Date Text */}
                <div className="my-2" style={{ fontWeight: '500', fontSize: '1.5rem', margin: '0.5rem 0' }}>
                    {date}
                </div>
                <Card.Text className="flex-grow-1 d-flex align-items-end justify-content-center">
                    {content}
                </Card.Text>
                {/* The button component */}
                <GeneralBottom
                    variant="secondary"
                    text="Send varsel"
                    className="mt-auto mb-3 mx-3"
                    style={{ padding: '10px 35px'}}
                />
            </Card.Body>
        </Card>
    );
}

export default RekoRingCard;
