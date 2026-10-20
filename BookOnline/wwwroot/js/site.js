// ========================================================
// HIỆU ỨNG TƯƠNG TÁC & CHUYỂN ĐỘNG (SITE.JS - PHƯƠNG NAM BOOK)
// ========================================================

document.addEventListener("DOMContentLoaded", function () {
    // 1. NÚT CUỘN LÊN ĐẦU TRANG (SCROLL TO TOP)
    const btnScrollTop = document.getElementById("btnScrollToTop");
    if (btnScrollTop) {
        window.addEventListener("scroll", function () {
            if (window.pageYOffset > 300) {
                btnScrollTop.classList.add("show");
            } else {
                btnScrollTop.classList.remove("show");
            }
        });

        btnScrollTop.addEventListener("click", function () {
            window.scrollTo({
                top: 0,
                behavior: "smooth"
            });
        });
    }

    // 2. TỰ ĐỘNG ẨN THÔNG BÁO FLASH MESSAGE SAU 4.5 GIÂY VỚI HIỆU ỨNG MƯỢT
    const alertElements = document.querySelectorAll(".alert.alert-dismissible");
    alertElements.forEach(function (alert) {
        setTimeout(function () {
            alert.style.transition = "opacity 0.6s ease, transform 0.6s ease";
            alert.style.opacity = "0";
            alert.style.transform = "translateY(-10px)";
            setTimeout(function () {
                alert.remove();
            }, 600);
        }, 4500);
    });

    // 3. HIỆU ỨNG RUNG GIỎ HÀNG KHI RÊ CHUỘT
    const cartWidget = document.querySelector(".pn-cart-widget");
    if (cartWidget) {
        cartWidget.addEventListener("mouseenter", function () {
            const cartImg = this.querySelector("img[alt='Giỏ hàng']");
            if (cartImg) {
                cartImg.style.animation = "cartWiggle 0.6s ease";
                setTimeout(() => { cartImg.style.animation = ""; }, 600);
            }
        });
    }

    // 4. XỬ LÝ "CHỌN MUA" QUA AJAX - Ở LẠI TRANG HIỆN TẠI & CẬP NHẬT GIỎ HÀNG TỨC THÌ
    function formatMoney(amount) {
        return Math.round(amount).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    }

    const addToCartForms = document.querySelectorAll(".form-add-to-cart");
    addToCartForms.forEach(function (form) {
        form.addEventListener("submit", function (e) {
            e.preventDefault();

            const btn = form.querySelector(".btn-pn-buy") || form.querySelector("button[type='submit']");
            const originalHtml = btn ? btn.innerHTML : "";
            const bookTitle = btn ? (btn.getAttribute("data-tensach") || "Sách") : "Sách";

            if (btn) {
                btn.style.transform = "scale(0.95)";
                btn.disabled = true;
                btn.innerHTML = '<i class="bi bi-check2-circle text-white"></i> <span>ĐÃ THÊM!</span>';
            }

            const formData = new FormData(form);

            fetch("/GioHang/ThemGioHang", {
                method: "POST",
                body: formData,
                headers: {
                    "X-Requested-With": "XMLHttpRequest"
                }
            })
            .then(res => res.json())
            .then(data => {
                if (data && data.success) {
                    // 1. Cập nhật số lượng trên badge Header
                    const headerBadge = document.getElementById("headerCartCount");
                    if (headerBadge) {
                        headerBadge.textContent = data.totalQty;
                        headerBadge.style.animation = "cartWiggle 0.6s ease";
                        setTimeout(() => { headerBadge.style.animation = ""; }, 600);
                    }

                    // 2. Cập nhật số lượng và tổng tiền trên Widget Giỏ hàng bên phải
                    const sidebarQty = document.getElementById("sidebarCartQty");
                    const sidebarAmount = document.getElementById("sidebarCartAmount");
                    if (sidebarQty) sidebarQty.textContent = data.totalQty;
                    if (sidebarAmount) {
                        sidebarAmount.innerHTML = `${formatMoney(data.totalAmount)} <span class="fs-6 text-dark">vnđ</span>`;
                    }

                    // 3. Hiệu ứng rung nhẹ giỏ hàng
                    const cartWidget = document.querySelector(".pn-cart-widget");
                    if (cartWidget) {
                        const cartImg = cartWidget.querySelector("img[alt='Giỏ hàng']");
                        if (cartImg) {
                            cartImg.style.animation = "cartWiggle 0.6s ease";
                            setTimeout(() => { cartImg.style.animation = ""; }, 600);
                        }
                    }

                    // 4. Bật Toast thông báo với nút xem giỏ hàng
                    showPnToast(
                        `🎉 Đã thêm <strong>"${data.tenSach || bookTitle}"</strong> vào giỏ! <a href="/GioHang" class="badge bg-warning text-dark text-decoration-none ms-1 px-2 py-1 fw-bold">Xem giỏ &rarr;</a>`,
                        "success",
                        "bi-cart-check-fill"
                    );
                } else {
                    showPnToast(data.message || "Không thể thêm vào giỏ hàng!", "danger", "bi-exclamation-circle-fill");
                }
            })
            .catch(err => {
                console.error("Add to cart error:", err);
                // Fallback nếu có lỗi mạng: submit thường
                form.submit();
            })
            .finally(() => {
                if (btn) {
                    setTimeout(() => {
                        btn.style.transform = "";
                        btn.disabled = false;
                        btn.innerHTML = originalHtml;
                    }, 1200);
                }
            });
        });
    });

    // 5. HỆ THỐNG TOAST THÔNG BÁO TƯƠNG TÁC (PHƯƠNG NAM TOAST)
    function showPnToast(message, type = "success", icon = "bi-check-circle-fill") {
        let toastContainer = document.getElementById("pnToastContainer");
        if (!toastContainer) {
            toastContainer = document.createElement("div");
            toastContainer.id = "pnToastContainer";
            toastContainer.className = "pn-toast-container";
            document.body.appendChild(toastContainer);
        }

        const toast = document.createElement("div");
        toast.className = `pn-toast toast-${type}`;
        toast.innerHTML = `<i class="bi ${icon} fs-5 text-${type === "success" ? "success" : type === "danger" ? "danger" : "primary"}"></i> <span>${message}</span>`;
        toastContainer.appendChild(toast);

        setTimeout(function () {
            toast.style.transition = "all 0.4s ease";
            toast.style.opacity = "0";
            toast.style.transform = "translateX(-30px)";
            setTimeout(() => { toast.remove(); }, 400);
        }, 3200);
    }


    // 7. XỬ LÝ NÚT TIM YÊU THÍCH (WISHLIST)
    const wishlistButtons = document.querySelectorAll(".btn-wishlist");
    wishlistButtons.forEach(function (btn) {
        btn.addEventListener("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            const bookName = btn.getAttribute("data-tensach") || "Sách";
            const isFav = btn.classList.toggle("active");
            
            if (isFav) {
                showPnToast(`❤️ Đã thêm <em>${bookName}</em> vào danh sách yêu thích!`, "danger", "bi-heart-fill");
            } else {
                showPnToast(`Đã bỏ <em>${bookName}</em> khỏi danh sách yêu thích.`, "info", "bi-info-circle-fill");
            }
        });
    });

    // 8. XỬ LÝ ẨN/HIỆN MẬT KHẨU (SHOW / HIDE PASSWORD)
    const togglePwButtons = document.querySelectorAll(".btn-toggle-pw");
    togglePwButtons.forEach(function (btn) {
        btn.addEventListener("click", function () {
            const targetId = btn.getAttribute("data-target");
            const input = document.getElementById(targetId);
            if (input) {
                const icon = btn.querySelector("i");
                if (input.type === "password") {
                    input.type = "text";
                    if (icon) {
                        icon.classList.remove("bi-eye");
                        icon.classList.add("bi-eye-slash");
                    }
                } else {
                    input.type = "password";
                    if (icon) {
                        icon.classList.remove("bi-eye-slash");
                        icon.classList.add("bi-eye");
                    }
                }
            }
        });
    });

    // 9. GIỚI HẠN CHỈ NHẬP SỐ CHO Ô ĐIỆN THOẠI
    const phoneInput = document.querySelector("input[name='DienThoai']");
    if (phoneInput) {
        phoneInput.addEventListener("input", function () {
            this.value = this.value.replace(/[^0-9]/g, '');
        });
    }

    // 10. GIỚI HẠN TÊN TÀI KHOẢN CHỈ KÝ TỰ KHÔNG DẤU VÀ SỐ TRÊN FORM ĐĂNG KÝ
    const usernameInput = document.querySelector("input[name='TaiKhoan']");
    if (usernameInput) {
        usernameInput.addEventListener("input", function () {
            if (window.location.pathname.toLowerCase().includes("register")) {
                this.value = this.value.replace(/[^a-zA-Z0-9_]/g, '');
            }
        });
    }
});

