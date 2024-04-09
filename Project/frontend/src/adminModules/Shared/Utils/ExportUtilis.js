export const processItemsForExport = (items, fieldsToRemove) => {
    return items.map(item => {
        const itemCopy = { ...item };
        fieldsToRemove.forEach(field => delete itemCopy[field]);
        return itemCopy;
    });
};