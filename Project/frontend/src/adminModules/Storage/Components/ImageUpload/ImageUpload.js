import React, { useState } from 'react';
import { Col, Image, Button } from 'react-bootstrap';
import { ReactComponent as BasicImage } from './basic-image.svg';
import './ImageUpload.css';
import GeneralBottom from "../../../Shared/Components/GeneralBottom/GeneralBottom";

import ReactCrop from 'react-image-crop';
import 'react-image-crop/dist/ReactCrop.css';

function ImageUpload({ onImageSelected }) {

    const [imageSrc, setImageSrc] = React.useState(null);

    // Handle image file selection
    const handleImageChange = event => {
        if (event.target.files && event.target.files[0]) {
            const file = event.target.files[0];
            setImageSrc(URL.createObjectURL(file));
            onImageSelected(file); // Pass the file up to the parent component
        }
    };




    return (
        <>
            {imageSrc ? (
                <div className="image-container">
                <Image src={imageSrc} alt="Uploaded Image" thumbnail />
                </div>
            ) : (
                // Display the SVG if no image has been selected
                <div className="image-placeholder">
                    <BasicImage />
                </div>
            )}
            <input
                type="file"
                accept="image/!*"
                onChange={handleImageChange}
                style={{ display: 'none' }}
                id="image-upload"
            />


            <div className="m-md-4 m-lg-2 mb-sm-5 mb-5">
            <GeneralBottom
                variant="primary"
                className="mt-2 btn-add-picture"
                text="+ Last opp bilde"
                onClick={() => document.getElementById('image-upload').click()}
            />
            </div>
        </>
    );
}



export default ImageUpload;
