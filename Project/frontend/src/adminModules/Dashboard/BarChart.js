import React from 'react';
import "./BarChart.css"
import { Bar } from 'react-chartjs-2';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';


ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
);


function BarChart({ data, options }) {
    return (
        <div className="chart-container">
            <Bar data={data} options={options} />
        </div>
    );
}


export default BarChart;
