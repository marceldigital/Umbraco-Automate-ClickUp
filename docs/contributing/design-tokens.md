# Design tokens

The Marcel Digital design system, vendored into this repository as the source of truth for the
documentation theme in `docs/template/public/main.css`.

> [!NOTE]
> The documentation site adopts the colour tokens, `Plus Jakarta Sans`, and the heading-weight
> logic below, but deliberately **not** the display type scale (102px/64px headings are hero
> typography, wrong for a reference page) and **not** the `Dark Blue` page theme (a saturated
> `#003FC8` reading surface fails contrast for links). See the comments in `main.css` for the
> three documented adaptations.

## Tokens

```yaml
name: Marcel Digital
colors:
  # Primary palette
  white: "#FFFFFF"
  orange: "#FF5A36"
  light-grey: "#E5EBF1"
  grey: "#D5D5D5"

  # Blue family
  blue: "#458FFF"
  dark-blue: "#003FC8"
  light-blue: "#DBE9FF"
  darkest-blue: "#060833"

  # UI utility
  blue-black: "#1C242E"
  blue-black-75: "#1C242EBF"
  btn-light-blue: "#B8D4FF"
  btn-light-blue-20: "#B8D4FF33"

  # Feedback
  error: "#E1191D"
  error-bg: "#E1191D1A"

  # Gradients (descriptive tokens)
  gradient-blue-overlay: "linear-gradient(360deg, rgba(0,29,227,0.75), rgba(0,29,227,0) 100%)"
  gradient-blue-full: "linear-gradient(360deg, rgba(0,29,227,0.75), rgba(0,29,227,0.75) 100%)"
  gradient-case-study-overlay: "linear-gradient(180deg, rgba(0,63,200,0.00) 9.86%, #003FC8 62.12%)"
  gradient-fireball: "radial-gradient(100% 100% at 50% 0%, #FFA02B 0%, rgba(255,160,43,0) 100%)"

  # Theme — White
  theme-white-bg: "#FFFFFF"
  theme-white-color: "#1C242E"
  theme-white-accent: "#003FC8"
  theme-white-border: "#D5D5D5"

  # Theme — Light Blue
  theme-light-blue-bg: "#DBE9FF"
  theme-light-blue-color: "#1C242E"
  theme-light-blue-accent: "#003FC8"
  theme-light-blue-border: "#D5D5D5"

  # Theme — Dark Blue
  theme-dark-blue-bg: "#003FC8"
  theme-dark-blue-color: "#FFFFFF"
  theme-dark-blue-accent: "#FFFFFF"
  theme-dark-blue-border: "transparent"

typography:
  display:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 52px
    fontWeight: "400"
    lineHeight: 1.23
    # Scales to 102px / 1.08 line-height at xl breakpoint (1400px+)
  h1:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 42px
    fontWeight: "400"
    lineHeight: 1.23
    # Scales to 102px / 1.08 at xl
  h2:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 42px
    fontWeight: "400"
    lineHeight: 1.21
    # Scales to 64px / 1.12 at xl
  h3:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 32px
    fontWeight: "400"
    lineHeight: 1.25
    # Scales to 48px / 1.20 at xl
  h4:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 24px
    fontWeight: "600"
    lineHeight: 1.25
    # Scales to 36px / 1.17 at xl
  h5:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 20px
    fontWeight: "600"
    lineHeight: 1.40
    # Scales to 24px / 1.33 at xl
  h6:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 18px
    fontWeight: "400"
    lineHeight: 1.55
    # Scales to 20px / 1.60 at xl
  body:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 18px
    fontWeight: "400"
    lineHeight: 1.5
  lead:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 18px
    fontWeight: "400"
    lineHeight: 1.55
    # Scales to 20px / 1.60 at xl
  text-lg:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 18px
    fontWeight: "400"
    lineHeight: 1.44
  text-md:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 14px
    fontWeight: "400"
    lineHeight: 16px
    # Scales to 16px / 1.25 at xl
  text-sm:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 14px
    fontWeight: "400"
    lineHeight: 1.25
  text-xs:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 12px
    fontWeight: "400"
    lineHeight: 16px
  nav-link:
    fontFamily: "Plus Jakarta Sans"
    fontSize: 20px
    fontWeight: "600"
    lineHeight: 1
    # Desktop nav shrinks to 16px / 400 weight

rounded:
  DEFAULT: 0.625rem   # 10px — form inputs
  md: 1.25rem         # 20px — cards, sections, service images
  lg: 1.5625rem       # 25px — testimonial cards, stat cards
  full: 6.25rem       # 100px — buttons (pill shape), icon circles

spacing:
  # Mobile spacing scale
  none: 0
  xs: 20px
  s: 30px
  m: 40px
  l: 60px
  xl: 60px
  # md+ spacing scale (overrides applied at 768px+)
  md-xs: 30px
  md-s: 40px
  md-m: 60px
  md-l: 80px
  md-xl: 100px
  # Layout
  card-padding: 32px
  card-padding-lg: 40px
  section-card-padding: 50px
  icon-card-body-padding: 40px
  testimonial-padding-mobile: 30px
  testimonial-padding-desktop: 50px
  footer-padding-top-mobile: 50px
  footer-padding-top-desktop: 60px
  nav-gap-lg: 40px
  nav-gap-xl: 50px
  container-gutter: 16px

breakpoints:
  xs: 0px
  md: 768px
  lg: 1180px
  xl: 1400px

containers:
  xs: 390px
  md: 720px
  lg: 1144px
  xl: 1312px

motion:
  base: "0.4s all ease-in-out"
  card-hover: "240ms ease"
  icon-hover: "250ms ease-in-out"
  cta-fade: "0.2s cubic-bezier(0.22, 0.61, 0.36, 1)"
  nav-collapse: "0.1s ease"
  rotate-step: "10s linear infinite"
  rotate-step-easing: "cubic-bezier(0.65, 0, 0.35, 1)"

elevation:
  card-hover-spread: "0 0 0 20px"
  card-hover-color: "#003FC8"

components:
  # Buttons
  button-primary:
    backgroundColor: "#FF5A36"
    textColor: "#FFFFFF"
    borderColor: "#FF5A36"
    rounded: "{rounded.full}"
    paddingY: 10px
    paddingX: 20px
  button-primary-lg:
    backgroundColor: "#FF5A36"
    textColor: "#FFFFFF"
    rounded: "{rounded.full}"
    paddingY: 14px
    paddingX: 67px
  button-secondary:
    backgroundColor: "#FF5A36"
    textColor: "#FFFFFF"
    rounded: "{rounded.full}"
  button-secondary-hover:
    backgroundColor: "#458FFF"
    borderColor: "#458FFF"
  button-transparent:
    backgroundColor: transparent
    textColor: "#FFFFFF"
    borderColor: "#FFFFFF"
    rounded: "{rounded.full}"
  button-transparent-hover:
    backgroundColor: "#458FFF"
    borderColor: "#458FFF"
  button-transparent-blue:
    backgroundColor: transparent
    textColor: "#003FC8"
    borderColor: "#003FC8"
    rounded: "{rounded.full}"
  button-transparent-blue-hover:
    backgroundColor: "#FF5A36"
    borderColor: "#FF5A36"
    textColor: "#FFFFFF"
  button-arrow:
    backgroundColor: "#B8D4FF"
    size: 40px
    rounded: "{rounded.full}"
  button-arrow-hover:
    backgroundColor: "#003FC8"

  # Cards
  card-blog-post:
    backgroundColor: "#FFFFFF"
    borderColor: "#D5D5D5"
    borderWidth: 1px
    rounded: "{rounded.md}"
    padding: 32px
    width: 413px
  card-blog-post-hover:
    backgroundColor: "#003FC8"
    borderColor: "#003FC8"
  card-service:
    backgroundColor: "transparent"
    borderColor: "#E5EBF1"
    borderWidth: 1px
    rounded: "{rounded.md}"
    padding: 25px
  card-testimonial:
    backgroundColor: "transparent"
    borderColor: "#D5D5D5"
    borderWidth: 1px
    rounded: "{rounded.lg}"
    paddingMobile: 30px
    paddingDesktop: 50px
    minHeight: 612px
  card-stat:
    backgroundColor: "#FFFFFF"
    borderColor: "#D5D5D5"
    borderWidth: 1px
    rounded: "{rounded.lg}"
    minHeight: 182px
    padding: "3px 20px"
  card-icon:
    backgroundColor: "var(--md-theme-bg-color)"
    borderColor: "var(--md-theme-border-color)"
    rounded: "{rounded.md}"
    bodyPadding: 40px
  section-card:
    borderColor: "#D5D5D5"
    borderWidth: 1px
    rounded: "{rounded.md}"
    padding: 50px

  # Form inputs
  input-field:
    backgroundColor: transparent
    borderColor: "var(--md-theme-form-border-color)"
    borderWidth: 1px
    rounded: "{rounded.DEFAULT}"
    padding: 15px
    fontSize: 16px
    lineHeight: "150%"
  input-error:
    borderColor: "#E1191D"
    backgroundColor: "#E1191D1A"
  form-check-input:
    size: 18px
    borderWidth: 2px
    checkedBg: "#003FC8"

  # Navigation
  header:
    backgroundColor: "#003FC8"
    navLinkColor: "#FFFFFF"
    navLinkWeight: "600"
    navLinkFontSize: 20px
  header-mobile-open:
    backgroundColor: "#FFFFFF"
  mega-menu-overlay-gradient: "linear-gradient(180deg, rgba(0,63,200,0.4) 0%, rgba(0,63,200,1) 100%)"

  # Footer
  footer:
    borderTop: "1px solid #D5D5D5"
    columnCount: 4
    paddingTopMobile: 50px
    paddingTopDesktop: 60px
    linkColor: "#1C242EBF"
    linkHoverColor: "#003FC8"

  # Decorative — "Fireball" orb
  fireball-orb:
    backgroundColor: "#FF5A36"
    shape: circle
    innerGradient: "{colors.gradient-fireball}"
    imageInset: 20px
    rotationAnimation: "{motion.rotate-step}"

  # Lists
  list-blue-check-bullets:
    iconColor: "#003FC8"
    iconBackgroundColor: "#003FC8"
    iconShape: circle
  list-orange-bolt-bullets:
    iconColor: "#FF5A36"
    iconBackgroundColor: "#FF5A36"
    iconShape: circle
```

## Brand & Style

Marcel Digital is a digital marketing agency whose visual identity communicates modern confidence, strategic depth, and bold energy. The design language pairs a restrained Swiss-influenced typographic system with high-contrast brand moments that feel unmistakably human and active.

The overarching aesthetic can be described as **clean-blue corporate with an orange spark** — the majority of the UI breathes in a cool, trust-building range of blues and white, while a single vivid orange (`#FF5A36`) fires into view on every call-to-action button and decorative element that demands attention. The tension between these two poles — the calm of deep navy and the urgency of orange — mirrors the agency's positioning: thoughtful strategy delivered with momentum.

## Colors

The palette has three distinct layers:

- **Action orange** (`#FF5A36`): The single warm accent. Used exclusively for primary CTAs, the arrow-icon background on hover, the "fireball" orb decoration, and custom bullet icons. Its purpose is to break visual monotony and direct the eye to conversion points.

- **Blue family** (five steps from `#DBE9FF` to `#060833`): The structural color range. `#003FC8` ("Marcel blue") is the header background, footer link headings, active form elements, and card hover states. `#458FFF` is a lighter action variant used on secondary button hovers. `#DBE9FF` is a soft surface tint for the light-blue page theme. `#060833` is the near-black for deep overlays.

- **Neutral foundation** (`#FFFFFF`, `#E5EBF1`, `#D5D5D5`, `#1C242E`): These handle borders, card backgrounds, body text, and footer fine print. The system avoids pure black, using `#1C242E` (blue-black) for all text to preserve tonal harmony with the blues.

### Themes

Three full-page color themes are defined and applied via a CSS custom-property mechanism (`--md-theme-*`):

| Theme | Background | Text | Accent |
|---|---|---|---|
| White | `#FFFFFF` | `#1C242E` | `#003FC8` |
| Light Blue | `#DBE9FF` | `#1C242E` | `#003FC8` |
| Dark Blue | `#003FC8` | `#FFFFFF` | `#FFFFFF` |

Any section or page block can adopt one of these themes. On the dark-blue theme, borders become transparent and form elements switch to a white stroke, ensuring all UI chrome remains legible against the deep background.

## Typography

The entire site uses a single typeface: **Plus Jakarta Sans** (loaded from Google Fonts, weight range 200–800). This humanist geometric sans-serif provides warmth at large display sizes while remaining highly readable at body scale.

The type system is **fluid and responsive** with two explicit size levels: a mobile baseline and a desktop (`xl`, 1400px+) expansion. Headings are deliberately set at `font-weight: 400` (regular) at display and H1–H3 scale, which creates an airy, editorial feel at large sizes. Functional hierarchy — H4, H5, navigation — steps up to `600` (semi-bold) to add visual grip.

Key sizing landmarks:
- **Display / H1 desktop**: 102px at 1.08 line-height — reserved for hero statements that function almost as full-bleed typography.
- **H2 desktop**: 64px — section headlines that carry the primary narrative.
- **H3 desktop**: 48px — subsection titles.
- **Body**: 18px at 1.5 line-height — generous measure that improves readability for long-form service and blog content.

Letter-spacing is left at the browser default for body and heading sizes; tracking is only introduced in the testimonial reviewer name via uppercase + `letter-spacing: 2px`, giving those attribution lines a label-like formality.

## Spacing

The spacing system uses a named T-shirt scale (`none`, `xs`, `s`, `m`, `l`, `xl`) rather than a unitless multiplier. Two parallel scales exist — one for mobile and one for `md+` — so spacing expands proportionally as the viewport grows without arbitrary overrides.

Horizontal rhythm is governed by Bootstrap's 12-column grid. Section containers cap at `1312px` on the largest breakpoint to prevent over-stretching text on ultra-wide displays, with a `1144px` intermediate cap at `lg` (1180px) for comfortable side margins on standard laptops.

All internal section padding and card padding is defined in `rem` units derived from a `rem()` SASS function, keeping everything relative to the root font size.

## Elevation & Depth

The system does not use a traditional shadow ladder. Instead, depth is achieved through two mechanisms:

1. **Expanding pseudo-element halo**: The blog post card uses a `::before` pseudo-element that starts flush (`inset: 0`) and expands to `inset: -20px` on hover, with `box-shadow: 0 0 0 0 #003FC8` — this creates a visible blue glow ring that grows outward from the card's perimeter on interaction.

2. **Background-fill reveal**: Most interactive cards animate their `background-color` from transparent/white to `#003FC8`, changing the entire card surface rather than lifting it. This grounds interaction firmly in the plane rather than floating elements over the page.

## Motion & Animation

Transitions follow a consistent **0.4s ease-in-out** base for theme changes (color, border, background). Interactive micromotion — card icon rotation, button arrow icon swap — uses the faster **250ms ease-in-out** so that hover feedback feels immediate.

The signature animation in the system is the **rotateStep** keyframe: a circular vector decoration (used in hero sections and alongside the fireball orb) steps through 90° increments every 2.5 seconds on a 10s loop using `cubic-bezier(0.65, 0, 0.35, 1)` easing. This cubic-bezier produces a brief ease-in / ease-out stutter at each quarter-turn that reads as mechanical precision — reinforcing the "strategic and systematic" brand personality — rather than an idle free-spin.

Navigation collapse on mobile uses a deliberately snappy `0.1s` height transition to feel responsive. The CTA button in the header fades out (`opacity: 0`) on a `0.2s cubic-bezier(0.22, 0.61, 0.36, 1)` when the mobile menu opens, clearing visual clutter without a jarring cut.

## Shapes & Iconography

The **"fireball" orb** is a defining decorative motif: a circular frame filled with `#FF5A36` and a radial gradient (`#FFA02B` at the top fading to transparent) surrounds a circular product/person image. This creates a warm, glowing effect that acts as a focal point in hero sections. The orb is always accompanied by a separate circular vector outline that slowly rotates underneath it.

Buttons and icon-containing circles uniformly use **full pill / 50% border-radius**, reinforcing a friendly, approachable quality that softens the otherwise crisp grid.

Cards and section containers use **20px or 25px** border-radius — large enough to feel contemporary and approachable, small enough to maintain structural authority.

Custom bullet icons replace default `<ul>` markers with two branded variants:
- A **blue circle with a check mark** (`#003FC8` background) for feature lists.
- An **orange circle with a lightning bolt** (`#FF5A36` background) for energy/capability lists.

Both icons are inline SVG data URIs that inherit the brand's primary and accent hues, ensuring they remain sharp at all densities without external assets.

## Components

### Buttons

All button styles are pill-shaped (`border-radius: 100px`). The primary button is always orange-on-white. The two transparent variants (white-stroke for dark-blue sections, dark-blue-stroke for light sections) flip to orange or blue fills on hover, maintaining brand consistency while adapting to any theme background. Arrow navigation buttons are a muted `#B8D4FF` at rest and deep-blue on hover, providing a non-aggressive directional control that doesn't compete with primary CTAs.

### Cards

Cards uniformly start transparent or white and animate to a filled state on hover. The blog post card reveals a deep-blue canvas with a simultaneously rotating orange arrow icon — a satisfying reward for exploration. Service cards display a diagonal arrow button that rotates 45° on hover, pointing "up and to the right" as a visual metaphor for growth. Testimonial and stat cards are static (no hover state) since they are read-only informational elements.

### Navigation

The header defaults to Marcel blue (`#003FC8`). On mobile, opening the mega-menu switches the bar to white to provide contrast for the dropdown overlay content. The mega-menu itself uses a blue gradient overlay that intensifies toward the bottom, anchoring the floating content visually to the bar above it.

### Forms

Form controls use a transparent background with a theme-aware border (`var(--md-theme-form-border-color)`), ensuring they are legible on any of the three page themes. Error state applies a red border and a 10% red tint background to the field — a gentle, non-alarming visual signal. Checkboxes fill solid Marcel blue on selection.
