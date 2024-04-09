import React from 'react';
import './GeneralBottom.css';
import { Button } from 'react-bootstrap';


// GeneralBottom component that takes text, variant, and onClick props
const GeneralBottom = ({ variant, text, onClick, className, style }) => {
    // Apply custom styles based on the variant prop
    const buttonClass = `btn-custom-${variant} ${className}`;

    return (
        <Button className={buttonClass} onClick={onClick} style={style}>
            {text}
        </Button>
    );
};

export default GeneralBottom;
