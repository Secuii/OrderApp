import fitz  # PyMuPDF
from PIL import Image, ImageDraw, ImageFont
import io

def add_watermark_to_pdf(pdf_bytes, watermark_text):
    # Open the original PDF
    pdf_document = fitz.open(stream=pdf_bytes, filetype="pdf")

    # Prepare to create the output PDF
    output_pdf_bytes = io.BytesIO()
    output_pdf_writer = fitz.open()  # Create a new PDF

    # Define the watermark font and size
    font_size = 100
    font = ImageFont.truetype('arial.ttf', font_size)

    for page_num in range(len(pdf_document)):
        page = pdf_document.load_page(page_num)

        # Render the page to an image
        pix = page.get_pixmap()
        img = Image.frombytes("RGB", [pix.width, pix.height], pix.samples)
        img = img.convert("RGBA")

        # Create an image for the watermark text
        text_image = Image.new('RGBA', img.size, (255, 255, 255, 0))
        text_draw = ImageDraw.Draw(text_image)

        # Calculate text size and position
        text_width = text_draw.textlength(watermark_text, font=font)
        text_height = text_draw.textlength(watermark_text, font=font)
        text_x = (text_image.width - text_width) / 2
        text_y = (text_image.height - text_height) / 2

        # Draw the text
        text_color = (255, 165, 0, 190)  # Orange with opacity
        text_draw.text((text_x, text_y), watermark_text, font=font, fill=text_color)

        # Rotate the text image by 45 degrees
        rotated_text_image = text_image.rotate(45, expand=1)

        # Paste the rotated text image onto the original image
        img.paste(rotated_text_image, (0, 0), rotated_text_image)

        # Convert the watermarked image back to PDF format
        img_byte_array = io.BytesIO()
        img.save(img_byte_array, format="PDF")
        img_byte_array.seek(0)

        # Add the processed image as a new page in the output PDF
        output_pdf_writer.insert_pdf(fitz.open(stream=img_byte_array.read(), filetype="pdf"))

    # Save the final watermarked PDF
    output_pdf_writer.save(output_pdf_bytes)
    output_pdf_writer.close()
    pdf_document.close()

    output_pdf_bytes.seek(0)
    return output_pdf_bytes.getvalue()


# Example usage:
with open('documento.pdf', 'rb') as pdf_file:
    pdf_bytes = pdf_file.read()

watermarked_pdf_bytes = add_watermark_to_pdf(pdf_bytes, "Pending        ")

# Save the result to a file to verify
with open('documento2.pdf', 'wb') as output_file:
    output_file.write(watermarked_pdf_bytes)
