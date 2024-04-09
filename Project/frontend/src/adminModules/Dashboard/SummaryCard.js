import React from 'react';
import { Card } from 'react-bootstrap';
import './SummaryCard.css';
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome"; // Your CSS file for styling

const SummaryCard = ({ title, value, icon, iconColor, background, sign }) => {
    return (
        <Card className="summary-card" style={{ background }}>
            <Card.Body>
                <Card.Title className="summary-card-title">{title}</Card.Title>
                <div className="summary-card-content">
                    <FontAwesomeIcon icon={icon} style={{ color: iconColor }} size="lg" />
                    <div className="summary-card-value">
                        {sign && <span className={`sign ${sign === '+' ? 'plus' : 'minus'}`}>{sign}</span>}
                        {value}
                    </div>
                </div>
            </Card.Body>
        </Card>
    );
};


export default SummaryCard;
