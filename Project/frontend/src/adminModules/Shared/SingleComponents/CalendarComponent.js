import React from 'react';
import { Button } from 'react-bootstrap';
import { FaCalendarAlt } from 'react-icons/fa';
import "../CSS/ButtonsColorComponent.css";
const CalendarButton = React.forwardRef(({ onClick }, ref) => {
    
    return (
        <Button variant="outline-secondary" onClick={onClick} className="me-2 mb-2 btn_botm">
            <FaCalendarAlt />
        </Button>
    );
});

export default CalendarButton;
